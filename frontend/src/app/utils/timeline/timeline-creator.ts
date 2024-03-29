import { HOURS_IN_DAY, MAX_DAYS_IN_MONTH, hasTimezone, intercept, toLocal } from '@utils/dates/dates.helper';
import { addDays, addHours, addMonths, addWeeks, addYears, daysInWeek, daysInYear, differenceInHours, differenceInYears, endOfDay, endOfMonth, endOfWeek, endOfYear, monthsInYear, startOfDay, startOfMonth, startOfWeek, startOfYear } from 'date-fns';

import { DatePipe } from '@angular/common';
import { Injectable } from '@angular/core';
import { JoinedTasksBlock } from '#types/dashboard/timeline/joined-tasks-block';
import { Task } from '#types/dashboard/tasks/task';
import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { TaskCompletion } from '#types/dashboard/tasks/task-completion';
import { TaskStatus } from '#types/dashboard/timeline/enums/task-status';
import { Timeline } from '#types/dashboard/timeline/timeline';
import { TimelineScale } from '#types/dashboard/timeline/enums/timeline-scale';
import { TimelineSection } from '#types/dashboard/timeline/timeline-section';

@Injectable({
    providedIn: 'root',
})
export class TimelineCreator {
    private readonly sectionsInDay = 6;
    private readonly minimumColumnNumber = 15;
    private readonly taskRowsNumber = 5;
    private readonly pageColumnNumber = 500;
    private boundaries!: TimelineSection;
    private columnNumber!: number;

    public create(currentDate: Date, scale: TimelineScale, pageNumber: number, tasks: Task[]): Timeline {
        this.boundaries = this.getTimelineBoundaries(currentDate, scale, pageNumber);
        this.columnNumber = pageNumber * this.pageColumnNumber;
        let timeline: Timeline = {
            columnNumber: this.columnNumber,
            pageNumber,
            tasksByRows: [],
            exceedingTaskBlocks: [],
            boundaries: this.boundaries,
            sections: this.getTimelineSections(this.boundaries, scale),
            subsections: this.getTimelineSubsections(this.boundaries, scale)
        };
        const tasksToAdd = this.getVisibleTasks(this.mapToBlocks(tasks)).map(task => this.addColumns(task));
        timeline = this.addTasks(tasksToAdd, timeline);
        return timeline;
    }

    private mapToBlocks(tasks: Task[]): TaskBlock[] {
        const taskBlocks: TaskBlock[] = [];
        for (const task of tasks) {
            for (const taskCompletion of task.taskCompletions) {
                const localTaskCompletion: TaskCompletion = hasTimezone(taskCompletion.start.toString())
                    ? {
                        ...taskCompletion,
                        start: new Date(taskCompletion.start),
                        end: new Date(taskCompletion.end)
                    }
                    : {
                        ...taskCompletion,
                        start: toLocal(new Date(taskCompletion.start)),
                        end: toLocal(new Date(taskCompletion.end))
                    };
                taskBlocks.push({
                    task,
                    taskCompletion: localTaskCompletion,
                    status: this.getBlockStatus(localTaskCompletion)
                });
            }
        }
        return taskBlocks.sort(
            (first, second) => first.taskCompletion.start.getTime() - second.taskCompletion.start.getTime()
        );
    }

    public getDateRatio(date: Date): number {
        return (date.getTime() - this.boundaries.start.getTime()) / this.getTimelineLength();
    }

    private addColumns(task: TaskBlock): TaskBlock {
        return { ...task, startColumn: this.getColumn(task.taskCompletion.start), endColumn: this.getColumn(task.taskCompletion.end) };
    }

    private getVisibleTasks(tasks: TaskBlock[]): TaskBlock[] {
        return tasks.filter(t => this.isVisible(t));
    }

    private isVisible(task: TaskBlock): boolean {
        return this.getColumn(task.taskCompletion.end) - this.getColumn(task.taskCompletion.start) >= this.minimumColumnNumber;
    }

    private getColumn(date: Date): number {
        if (date < this.boundaries.start) {
            return 1;
        }
        if (date > this.boundaries.end) {
            return this.columnNumber + 1;
        }
        return Math.round(this.getDateRatio(date) * this.columnNumber) + 1;
    }

    private getBlockStart(block: TaskBlock[]): Date {
        return block.sort(
            (first, second) => first.taskCompletion.start.getTime() - second.taskCompletion.start.getTime()
        )[0].taskCompletion.start;
    }

    private getBlockEnd(block: TaskBlock[]): Date {
        return block.sort(
            (first, second) => second.taskCompletion.end.getTime() - first.taskCompletion.end.getTime()
        )[0].taskCompletion.end;
    }

    private getTimelineLength(): number {
        return this.boundaries.end.getTime() - this.boundaries.start.getTime();
    }

    private addTasks(tasks: TaskBlock[], timeline: Timeline): Timeline {
        const tasksByRows: TaskBlock[][] = Array.from(Array(this.taskRowsNumber), () => []);
        const exceeding: TaskBlock[][] = [];
        for (const task of tasks) {
            let inserted = false;
            for (let i = 0; i < tasksByRows.length && !inserted; i++) {
                if (!tasksByRows[i].find(t => intercept(t.taskCompletion, task.taskCompletion))) {
                    tasksByRows[i].push(task);
                    inserted = true;
                }
            }
            if (!inserted) {
                this.addToExceeding(exceeding, task);
            }
        }
        let exceedingTaskBlocks: JoinedTasksBlock[] = [];
        if (exceeding.length > 0) {
            exceedingTaskBlocks = this.getExceedingBlocks(exceeding);
        }
        return { ...timeline, tasksByRows, exceedingTaskBlocks };
    }

    private getExceedingBlocks(exceeding: TaskBlock[][]): JoinedTasksBlock[] {
        let exceedingTaskBlocks: TaskBlock[][] = [];
        const sortedExceeding = exceeding.sort(
            (firstBlock, secondBlock) => this.getBlockStart(firstBlock).getTime() - this.getBlockStart(secondBlock).getTime()
        );
        let lastBlockIndex = 0;
        exceedingTaskBlocks = [sortedExceeding[0]];
        for (let i = 1; i < sortedExceeding.length; i++) {
            if (this.getBlockEnd(exceedingTaskBlocks[lastBlockIndex]) > this.getBlockStart(sortedExceeding[i])) {
                exceedingTaskBlocks[lastBlockIndex] = exceedingTaskBlocks[lastBlockIndex].concat(sortedExceeding[i]);
            } else {
                exceedingTaskBlocks.push(sortedExceeding[i]);
                lastBlockIndex++;
            }
        }
        return exceedingTaskBlocks.map(tasks => ({
            tasks,
            expanded: false,
            startColumn: this.getColumn(this.getBlockStart(tasks)),
            endColumn: this.getColumn(this.getBlockEnd(tasks))
        }));
    }

    private addToExceeding(exceeding: TaskBlock[][], task: TaskBlock): void {
        let inserted = false;
        for (let i = 0; i < exceeding.length && !inserted; i++) {
            const block = exceeding[i];
            if (intercept(task.taskCompletion, { start: this.getBlockStart(block), end: this.getBlockEnd(block) })) {
                block.push(task);
                inserted = true;
            }
        }
        if (!inserted) {
            exceeding.push([task]);
        }
    }

    private getTimelineBoundaries(currentDate: Date, scale: TimelineScale, pageNumber: number): TimelineSection {
        switch (scale) {
        case TimelineScale.Day:
            return {
                start: startOfDay(addDays(currentDate, -1 * pageNumber / 3)),
                end: endOfDay(addDays(currentDate, pageNumber / 3))
            };
        case TimelineScale.Week:
            return {
                start: startOfWeek(addWeeks(currentDate, -1 * pageNumber / 2)),
                end: endOfWeek(addWeeks(currentDate, pageNumber / 2))
            };
        case TimelineScale.Month:
            return {
                start: startOfMonth(addMonths(currentDate, -1 * pageNumber / 2)),
                end: endOfMonth(addMonths(currentDate, pageNumber / 2))
            };
        case TimelineScale.Year:
            return {
                start: startOfMonth(addYears(currentDate, -1 * pageNumber / 2)),
                end: endOfMonth(addYears(currentDate, pageNumber / 2))
            };
        default:
            throw new Error('Invalid scale');
        }
    }

    private getTimelineSections(boundaries: TimelineSection, scale: TimelineScale) {
        if (scale !== TimelineScale.Year) {
            return this.getTimelineSubsections(boundaries, scale + 1).map(section =>
                ({
                    ...section,
                    title: this.getSectionTitle(section, scale)
                }));
        }
        const sectionHours = HOURS_IN_DAY * (daysInYear + 2);
        const sectionsCount = differenceInYears(boundaries.end, boundaries.start) + 1;
        const sections: TimelineSection[] = [];
        const firstSectionStart = startOfYear(boundaries.start);
        sections.push({ start: firstSectionStart, end: endOfYear(firstSectionStart) });
        for (let i = 1; i < sectionsCount; i++) {
            const previousDate = sections[i - 1].start;
            const section = {
                start: startOfYear(addHours(previousDate, sectionHours)),
                end: endOfYear(addHours(previousDate, sectionHours)),
            };
            sections.push(section);
        }
        return sections.map(section => ({
            ...section,
            title: this.getSectionTitle(section, scale),
            startColumn: this.getColumn(section.start),
            endColumn: this.getColumn(section.end)
        }));
    }

    private getTimelineSubsections(boundaries: TimelineSection, scale: TimelineScale): TimelineSection[] {
        const sectionHours: number = this.getSubsectionSize(scale);
        let sectionsCount: number = Math.round(differenceInHours(boundaries.end, boundaries.start) / sectionHours);
        if (scale === TimelineScale.Month) {
            sectionsCount++;
        }
        if (scale == TimelineScale.Year) {
            const years = differenceInYears(boundaries.end, boundaries.start);
            sectionsCount = years > 0 ? years * monthsInYear : sectionsCount;
        }
        const sections: TimelineSection[] = [];
        const startFunction = this.getStartFunction(scale);
        const endFunction = this.getEndFunction(scale);

        const firstSectionStart = startFunction(boundaries.start);
        sections.push({ start: firstSectionStart, end: endFunction(firstSectionStart) });
        for (let i = 1; i < sectionsCount; i++) {
            const previousDate = sections[i - 1].start;
            const section = scale !== TimelineScale.Week ? {
                start: startFunction(addHours(previousDate, sectionHours)),
                end: endFunction(addHours(previousDate, sectionHours)),
            } : {
                start: startFunction(addHours(startOfDay(previousDate), sectionHours + 2)),
                end: endFunction(addHours(startOfDay(previousDate), sectionHours + 2))
            };

            sections.push(section);
        }
        return sections.map(section => ({
            ...section,
            title: this.getSubsectionTitle(section, scale),
            startColumn: this.getColumn(section.start),
            endColumn: this.getColumn(section.end)
        }));
    }

    private getSubsectionTitle(subsection: TimelineSection, scale: TimelineScale): string {
        const datePipe = new DatePipe('en-US');
        switch (scale) {
        case TimelineScale.Day:
            return `${datePipe.transform(subsection.start, 'HH:mm')} - ${datePipe.transform(subsection.end, 'HH:mm')}`;
        case TimelineScale.Week:
            return datePipe.transform(subsection.start, 'EEEE')!;
        case TimelineScale.Month:
            return `${datePipe.transform(subsection.start, 'MM/dd')} - ${datePipe.transform(subsection.end, 'MM/dd')}`;
        case TimelineScale.Year:
            return datePipe.transform(subsection.start, 'MMMM')!;
        default:
            throw new Error('Invalid scale');
        }
    }

    private getSectionTitle(section: TimelineSection, scale: TimelineScale): string {
        const datePipe = new DatePipe('en-US');
        switch (scale) {
        case TimelineScale.Day:
            return datePipe.transform(section.start, 'MM/dd/YYYY')!;
        case TimelineScale.Week:
            return `${datePipe.transform(section.start, 'MM/dd/YYYY')} - ${datePipe.transform(section.end, 'MM/dd/YYYY')}`;
        case TimelineScale.Month:
            return datePipe.transform(section.start, 'MMMM, YYYY')!;
        case TimelineScale.Year:
            return section.start.getFullYear().toString();
        default:
            throw new Error('Invalid scale');
        }
    }

    private getSubsectionSize(scale: TimelineScale) {
        switch (scale) {
        case TimelineScale.Day:
            return HOURS_IN_DAY / this.sectionsInDay;
        case TimelineScale.Week:
            return HOURS_IN_DAY;
        case TimelineScale.Month:
            return HOURS_IN_DAY * daysInWeek + 1;
        case TimelineScale.Year:
            return HOURS_IN_DAY * (MAX_DAYS_IN_MONTH + 1);
        default:
            throw new Error('Invalid scale');
        }
    }

    private getStartFunction(scale: TimelineScale) {
        switch (scale) {
        case TimelineScale.Day:
            return (date: Date) => date;
        case TimelineScale.Week:
            return startOfDay;
        case TimelineScale.Month:
            return startOfWeek;
        case TimelineScale.Year:
            return startOfMonth;
        default:
            throw new Error('Invalid scale');
        }
    }

    private getEndFunction(scale: TimelineScale) {
        switch (scale) {
        case TimelineScale.Day:
            return (date: Date) => addHours(date, 4);
        case TimelineScale.Week:
            return endOfDay;
        case TimelineScale.Month:
            return endOfWeek;
        case TimelineScale.Year:
            return endOfMonth;
        default:
            throw new Error('Invalid scale');
        }
    }

    private getBlockStatus(taskCompletion: TaskCompletion): TaskStatus {
        if (taskCompletion.isSuccessful) {
            return TaskStatus.Completed;
        }
        if (taskCompletion.end < new Date()) {
            return TaskStatus.Failed;
        }
        if (taskCompletion.start > new Date()) {
            return TaskStatus.ToDo;
        }
        return TaskStatus.InProgress;
    }
}
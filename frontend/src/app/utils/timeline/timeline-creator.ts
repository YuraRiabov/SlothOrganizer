import { addDays, addHours, addMonths, addWeeks, addYears, daysInWeek, daysInYear, differenceInHours, differenceInYears, endOfDay, endOfMonth, endOfWeek, endOfYear, monthsInYear, startOfDay, startOfMonth, startOfWeek, startOfYear } from 'date-fns';
import { hoursInDay, isBetween, maxDaysInMonth } from '@utils/dates/dates.helper';

import { DatePipe } from '@angular/common';
import { Injectable } from '@angular/core';
import { Task } from '#types/tasks/task';
import { TasksBlock } from '#types/tasks/timeline/tasks-block';
import { Timeline } from '#types/tasks/timeline/timeline';
import { TimelineScale } from '#types/tasks/timeline/enums/timeline-scale';
import { TimelineSection } from '#types/tasks/timeline/timeline-section';

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
        timeline = this.addTasks(this.getVisibleTasks(tasks), timeline);
        return timeline;
    }

    public getDateRatio(date: Date): number {
        return (date.getTime() - this.boundaries.start.getTime()) / this.getTimelineLength();
    }
    public getVisibleTasks(tasks: Task[]): Task[] {
        return tasks.filter(t => this.isVisible(t));
    }

    private isVisible(task: Task): boolean {
        return this.getColumn(task.end) - this.getColumn(task.start) >= this.minimumColumnNumber;
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

    private getBlockStart(block: Task[]): Date {
        return block.sort((first, second) => first.start.getTime() - second.start.getTime())[0].start;
    }

    private getBlockEnd(block: Task[]): Date {
        return block.sort((first, second) => second.end.getTime() - first.end.getTime())[0].end;
    }

    private getTimelineLength(): number {
        return this.boundaries.end.getTime() - this.boundaries.start.getTime();
    }

    private addTasks(tasks: Task[], timeline: Timeline): Timeline {
        let tasksByRows: Task[][] = Array.from(Array(this.taskRowsNumber), () => []);
        let exceeding: Task[][] = [];
        for (let task of tasks) {
            let inserted = false;
            for (let i = 0; i < tasksByRows.length && !inserted; i++) {
                if (!tasksByRows[i].find(t => isBetween(t.end, task.start, task.end) ||
                    isBetween(task.end, t.start, t.end))) {
                    tasksByRows[i].push(task);
                    inserted = true;
                }
            }
            if (!inserted) {
                this.addToExceeding(exceeding, task);
            }
        }
        let exceedingTaskBlocks: TasksBlock[] = [];
        if (exceeding.length > 0) {
            exceedingTaskBlocks = this.getExceedingBlocks(exceeding);
        }
        return { ...timeline, tasksByRows, exceedingTaskBlocks };
    }

    private getExceedingBlocks(exceeding: Task[][]): TasksBlock[] {
        let exceedingTaskBlocks: Task[][] = [];
        exceeding = exceeding.sort(
            (firstBlock, secondBlock) => this.getBlockStart(firstBlock).getTime() - this.getBlockStart(secondBlock).getTime()
        );
        let lastBlockIndex = 0;
        exceedingTaskBlocks = [exceeding[0]];
        for (let i = 1; i < exceeding.length; i++) {
            if (this.getBlockEnd(exceedingTaskBlocks[lastBlockIndex]) > this.getBlockStart(exceeding[i])) {
                exceedingTaskBlocks[lastBlockIndex] = exceedingTaskBlocks[lastBlockIndex].concat(exceeding[i]);
            } else {
                exceedingTaskBlocks.push(exceeding[i]);
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

    private addToExceeding(exceeding: Task[][], task: Task): void {
        let inserted = false;
        for (let i = 0; i < exceeding.length && !inserted; i++) {
            let block = exceeding[i];
            if (isBetween(task.end, this.getBlockStart(block), this.getBlockEnd(block)) ||
                isBetween(this.getBlockEnd(block), task.start, task.end)) {
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
            return this.getTimelineSubsections(boundaries, scale + 1);
        }
        let sectionHours = hoursInDay * (daysInYear + 2);
        let sectionsCount = differenceInYears(boundaries.end, boundaries.start);
        let sections: TimelineSection[] = [];
        let firstSectionStart = startOfYear(boundaries.start);
        sections.push({ start: firstSectionStart, end: endOfYear(firstSectionStart) });
        for (let i = 1; i < sectionsCount; i++) {
            let previousDate = sections[i - 1].start;
            let section = {
                start: startOfYear(addHours(previousDate, sectionHours)),
                end: endOfYear(addHours(previousDate, sectionHours)),
            };
            sections.push(section);
        }
        return sections.map(section => ({ ...section, title: this.getSectionTitle(section, scale) }));
    }

    private getTimelineSubsections(boundaries: TimelineSection, scale: TimelineScale): TimelineSection[] {
        const sectionHours: number = this.getSubsectionSize(scale);
        let sectionsCount: number = Math.round(differenceInHours(boundaries.end, boundaries.start) / sectionHours);
        if (scale === TimelineScale.Month) {
            sectionsCount++;
        }
        if (scale == TimelineScale.Year) {
            sectionsCount = differenceInYears(boundaries.end, boundaries.start) * monthsInYear;
        }
        let sections: TimelineSection[] = [];
        const startFunction = this.getStartFunction(scale);
        const endFunction = this.getEndFunction(scale);

        const firstSectionStart = startFunction(boundaries.start);
        sections.push({ start: firstSectionStart, end: endFunction(firstSectionStart) });
        for (let i = 1; i < sectionsCount; i++) {
            let previousDate = sections[i - 1].start;
            let section = {
                start: startFunction(addHours(previousDate, sectionHours)),
                end: endFunction(addHours(previousDate, sectionHours)),
            };
            sections.push(section);
        }
        return sections.map(section => ({ ...section, title: this.getSubsectionTitle(section, scale) }));
    }

    private getSubsectionTitle(subsection: TimelineSection, scale: TimelineScale): string {
        let datePipe = new DatePipe('en-US');
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
        let datePipe = new DatePipe('en-US');
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
            return hoursInDay / this.sectionsInDay;
        case TimelineScale.Week:
            return hoursInDay;
        case TimelineScale.Month:
            return hoursInDay * daysInWeek + 1;
        case TimelineScale.Year:
            return hoursInDay * (maxDaysInMonth + 1);
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
}
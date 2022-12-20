import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { getSubsectionTitle, getTimelineBoundaries, getTimelineSubsections } from '@utils/dates/timeline.helper';

import { Task } from '#types/tasks/task';
import { TimelineScale } from '#types/tasks/enums/timeline-scale';
import { TimelineSection } from '#types/tasks/timeline-section';
import { addDays } from 'date-fns';
import { isBetween } from '@utils/dates/dates.helper';

@Component({
    selector: 'app-timeline',
    templateUrl: './timeline.component.html',
    styleUrls: ['./timeline.component.sass']
})
export class TimelineComponent implements OnInit {
    private readonly minimumColumnNumber = 25;
    public readonly columnNumber = 500;

    private timelineScale: TimelineScale = TimelineScale.Day;

    @Input() public currentDate: Date = new Date();
    @Input() public tasks: Task[] = [];
    @Input() public set scale(value: TimelineScale) {
        this.timelineScale = value;
        this.initializeTimeline();
    }

    @Output() scaleIncreased = new EventEmitter<Date>();

    public tasksByRows: Task[][] = [];
    public timelineBoundaries: TimelineSection = {} as TimelineSection;
    public timelineSubsections: TimelineSection[] = [];

    constructor() { }

    ngOnInit(): void {
        this.initializeTimeline();
    }

    public getColumn(date: Date): number {
        const result = Math.round((date.getTime() - this.timelineBoundaries.start.getTime()) / this.getTimelineLength() * this.columnNumber);
        return result > 0 ? result + 1 : 1;
    }

    public getVisibleTasks(): Task[] {
        return this.tasks.filter(t => this.isVisible(t));
    }

    public getSubsectionTitle(subsection: TimelineSection): string {
        return getSubsectionTitle(subsection, this.timelineScale);
    }

    public increaseScale(section: Date) {
        if (this.timelineScale === TimelineScale.Month) {
            this.scaleIncreased.emit(addDays(section, 1));
            return;
        }
        this.scaleIncreased.emit(section);
    }

    private initializeTimeline(): void {
        this.timelineBoundaries = getTimelineBoundaries(this.currentDate, this.timelineScale);
        this.tasksByRows = this.separateTasksByRows(this.getVisibleTasks());
        this.timelineSubsections = getTimelineSubsections(this.timelineBoundaries, this.timelineScale);
    }

    private isVisible(task: Task): boolean {
        return this.getColumn(task.end) - this.getColumn(task.start) >= this.minimumColumnNumber;
    }

    private getTimelineLength(): number {
        return this.timelineBoundaries.end.getTime() - this.timelineBoundaries.start.getTime();
    }

    private separateTasksByRows(tasks: Task[]): Task[][] {
        let tasksByRows: Task[][] = Array.from(Array(5), () => []);
        for (let task of tasks) {
            let inserted = false;
            for (let i = 0; i < tasksByRows.length && !inserted; i++) {
                if (!tasksByRows[i].find(t => isBetween(t.end, task.start, task.end) ||
                    isBetween(task.end, t.start, t.end))) {
                    tasksByRows[i].push(task);
                    inserted = true;
                }
            }
        }
        return tasksByRows;
    }
}

import { Component, Input, OnInit } from '@angular/core';
import { getSubsectionTitle, getTimelineBoundaries, getTimelineSubsections } from '@utils/dates/timeline.helper';

import { Task } from '#types/tasks/task';
import { TimelineBoundaries } from '#types/tasks/timeline-boundaries';
import { TimelineScale } from '#types/tasks/enums/timeline-scale';
import { isBetween } from '@utils/dates/dates.helper';

@Component({
    selector: 'app-timeline',
    templateUrl: './timeline.component.html',
    styleUrls: ['./timeline.component.sass']
})
export class TimelineComponent implements OnInit {
    private readonly minimumColumnNumber = 4;
    public readonly columnNumber = 84;

    @Input() public currentDate: Date = new Date();
    @Input() public tasks: Task[] = [];
    @Input() public scale: TimelineScale = TimelineScale.Day;

    public tasksByRows: Task[][] = [];
    public timelineBoundaries: TimelineBoundaries = {} as TimelineBoundaries;
    public timelineSubsections: Date[] = [];

    constructor() { }

    ngOnInit(): void {
        this.timelineBoundaries = getTimelineBoundaries(this.currentDate, this.scale);
        this.tasksByRows = this.separateTasksByRows(this.getVisibleTasks());
        this.timelineSubsections = getTimelineSubsections(this.timelineBoundaries, this.scale);
    }

    public getStartColumn(task: Task) : number {
        return Math.floor((task.start.getTime() - this.timelineBoundaries.start.getTime())/this.getTimelineLength()*this.columnNumber);
    }

    public getEndColumn(task: Task) : number {
        return Math.ceil((task.end.getTime() - this.timelineBoundaries.start.getTime())/this.getTimelineLength()*this.columnNumber);
    }

    public getVisibleTasks(): Task[] {
        return this.tasks.filter(t => this.isVisible(t));
    }

    public getSubsectionTitle(subsection: Date): string {
        return getSubsectionTitle(subsection, this.scale);
    }

    private isVisible(task: Task) : boolean {
        return this.getEndColumn(task) - this.getStartColumn(task) >= this.minimumColumnNumber;
    }

    private getTimelineLength() : number {
        return this.timelineBoundaries.end.getTime() - this.timelineBoundaries.start.getTime();
    }

    private separateTasksByRows(tasks: Task[]) : Task[][] {
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

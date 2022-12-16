import { Component, Input } from '@angular/core';

import { Task } from '#types/tasks/task';

@Component({
    selector: 'app-timeline',
    templateUrl: './timeline.component.html',
    styleUrls: ['./timeline.component.sass']
})
export class TimelineComponent {
    public readonly columnNumber = 84;
    public timelineStart: Date = new Date(2022, 12, 16);
    public timelineEnd: Date = new Date(2022, 12, 17);

    @Input() public tasks: Task[] = [];

    constructor() { }

    public getStartColumn(task: Task) : number {
        return Math.ceil((task.start.getTime() - this.timelineStart.getTime())/this.getTimelineLength()*this.columnNumber);
    }

    public getEndColumn(task: Task) : number {
        return Math.ceil((task.end.getTime() - this.timelineStart.getTime())/this.getTimelineLength()*this.columnNumber);
    }

    private getTimelineLength() : number {
        return this.timelineEnd.getTime() - this.timelineStart.getTime();
    }
}

import { Component } from '@angular/core';
import { Task } from '#types/tasks/task';
import { TimelineScale } from '#types/tasks/enums/timeline-scale';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent {
    public tasks: Task[] = [
        {
            start: new Date(2022, 11, 16, 8),
            end: new Date(2022, 13, 1, 0),
            title: 'month block'
        },
        {
            start: new Date(2022, 12, 16, 8),
            end: new Date(2022, 12, 22, 0),
            title: 'big block'
        },
        {
            start: new Date(2022, 12, 16, 8),
            end: new Date(2022, 12, 16, 11),
            title: 'first block'
        },
        {
            start: new Date(2022, 12, 16, 15),
            end: new Date(2022, 12, 16, 17),
            title: 'second block'
        },
        {
            start: new Date(2022, 12, 16, 10),
            end: new Date(2022, 12, 16, 23),
            title: 'third block'
        },
        {
            start: new Date(2022, 12, 16, 12),
            end: new Date(2022, 12, 16, 18),
            title: 'fourth block'
        },
        {
            start: new Date(2022, 12, 16, 13),
            end: new Date(2022, 12, 16, 20),
            title: 'fifth block'
        },
        {
            start: new Date(2022, 12, 16, 14),
            end: new Date(2022, 12, 16, 15),
            title: 'sixth block'
        },
        {
            start: new Date(2022, 12, 16, 14),
            end: new Date(2022, 12, 16, 15),
            title: 'sixth block'
        },
        {
            start: new Date(2022, 12, 16, 14),
            end: new Date(2022, 12, 16, 15),
            title: 'sixth block'
        },
        {
            start: new Date(2022, 12, 16, 14),
            end: new Date(2022, 12, 16, 15),
            title: 'sixth block'
        },
        {
            start: new Date(2022, 12, 16, 14),
            end: new Date(2022, 12, 16, 15),
            title: 'sixth block'
        },
        {
            start: new Date(2022, 12, 16, 10),
            end: new Date(2022, 12, 16, 12, 30),
            title: 'sixth block'
        },
        {
            start: new Date(2022, 12, 16, 10, 30),
            end: new Date(2022, 12, 16, 13, 30),
            title: 'sixth block'
        },
        {
            start: new Date(2022, 12, 16, 13),
            end: new Date(2022, 12, 16, 13, 30),
            title: 'sixth block'
        }
    ];

    public currentDate: Date = new Date(2022, 12, 16, 23);
    public timelineScale = TimelineScale.Day;
    constructor() { }

    public zoomIn(date? : Date) : void {
        this.currentDate = date ?? this.currentDate;
        if (this.timelineScale != TimelineScale.Day) {
            this.timelineScale--;
        }
    }

    public zoomOut() : void {
        if (this.timelineScale != TimelineScale.Year) {
            this.timelineScale++;
        }
    }
}

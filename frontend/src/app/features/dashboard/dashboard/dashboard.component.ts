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
            start: new Date(2022, 12, 16, 8),
            end: new Date(2022, 12, 21, 18),
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
            title: 'second block'
        }
    ];

    public currentDate = new Date(2022, 12, 16, 23);
    public timelineScale = TimelineScale.Month;
    constructor() { }
}

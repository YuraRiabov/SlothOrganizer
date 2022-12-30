import * as dashboardActions from '@store/actions/dashboard.actions';

import { Component, OnInit } from '@angular/core';

import { BaseComponent } from '@shared/components/base/base.component';
import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { MatSelectChange } from '@angular/material/select';
import { Store } from '@ngrx/store';
import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { TimelineScale } from '#types/dashboard/timeline/enums/timeline-scale';
import { addHours } from 'date-fns';
import { getDefaultDashboard } from '@utils/creation-functions/dashboard-creation.helper';
import { selectDashboards } from '@store/selectors/dashboard.selectors';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent extends BaseComponent implements OnInit {
    public tasks: TaskBlock[] = [
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
        }
    ];

    public currentDate: Date = new Date(2022, 12, 16, 12);
    public timelineScale = TimelineScale.Day;

    public dashboards: Dashboard[] = [];
    public currentDashboard: Dashboard = getDefaultDashboard();

    public creatingDashboard: boolean = false;

    constructor(private store: Store) {
        super();
    }

    ngOnInit(): void {
        this.store.dispatch(dashboardActions.loadDashboards());

        this.store.select(selectDashboards)
            .pipe(this.untilDestroyed)
            .subscribe(dashboards => {
                this.dashboards = dashboards;
                if (dashboards.length > 0) {
                    this.currentDashboard = dashboards[0];
                    this.store.dispatch(dashboardActions.chooseDashboard({ dashboardId: this.currentDashboard.id }));
                }
            });
    }

    public selectDashboard(selection: MatSelectChange) {
        this.currentDashboard = selection.value as Dashboard;
        this.store.dispatch(dashboardActions.chooseDashboard({ dashboardId: this.currentDashboard.id }));
    }

    public zoomIn(date?: Date): void {
        this.currentDate = date ?? this.currentDate;
        if (this.timelineScale != TimelineScale.Day) {
            this.timelineScale--;
        }
    }

    public zoomOut(): void {
        if (this.timelineScale != TimelineScale.Year) {
            this.timelineScale++;
        }
    }

    public goToDate(event: MatDatepickerInputEvent<Date>) {
        this.currentDate = addHours(event.value!, 12);
    }
}

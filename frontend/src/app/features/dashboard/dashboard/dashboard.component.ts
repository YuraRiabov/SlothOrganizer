import * as dashboardActions from '@store/actions/dashboard.actions';

import { Component, OnInit } from '@angular/core';
import { Observable, map, merge, of, shareReplay, switchMap } from 'rxjs';
import { selectChosenDashboard, selectChosenDashboardId, selectDashboards } from '@store/selectors/dashboard.selectors';

import { BaseComponent } from '@shared/components/base/base.component';
import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { MatSelectChange } from '@angular/material/select';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';
import { Store } from '@ngrx/store';
import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { TimelineScale } from '#types/dashboard/timeline/enums/timeline-scale';
import { addHours } from 'date-fns';
import { getDefaultDashboard } from '@utils/creation-functions/dashboard-creation.helper';

@Component({
    selector: 'so-dashboard',
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

    public dashboards$?: Observable<Dashboard[]>;
    public currentDashboard$?: Observable<Dashboard>;

    public creatingDashboard: boolean = false;
    public sidebarOpen: boolean = false;
    public sidebarType: SidebarType = SidebarType.Create;

    constructor(private store: Store) {
        super();
    }

    ngOnInit(): void {
        this.store.dispatch(dashboardActions.loadDashboards());

        this.dashboards$ = this.store.select(selectDashboards);
        this.currentDashboard$ = this.store.select(selectChosenDashboard);
    }

    public openCreateSidebar(): void {
        this.openSidebar(SidebarType.Create);
    }

    public selectDashboard(dashboard: Dashboard): void {
        this.store.dispatch(dashboardActions.chooseDashboard({ dashboardId: dashboard.id }));
    }

    public createDashboard(title: string): void {
        this.store.dispatch(dashboardActions.createDashbaord({ title }));
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

    public goToDate(event: MatDatepickerInputEvent<Date>): void {
        this.currentDate = addHours(event.value!, 12);
    }

    private openSidebar(type: SidebarType): void {
        this.sidebarType = type;
        this.sidebarOpen = true;
    }
}

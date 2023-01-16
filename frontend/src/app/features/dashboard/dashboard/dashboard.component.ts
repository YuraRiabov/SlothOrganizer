import * as dashboardActions from '@store/actions/dashboard.actions';

import { Component, OnInit } from '@angular/core';
import { selectChosenDashboardId, selectDashboards } from '@store/selectors/dashboard.selectors';

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
    public currentDate: Date = new Date();
    public timelineScale = TimelineScale.Day;

    public dashboards: Dashboard[] = [];
    public currentDashboard: Dashboard = getDefaultDashboard();

    public creatingDashboard: boolean = false;
    public sidebarOpen: boolean = false;
    public sidebarType: SidebarType = SidebarType.Create;

    constructor(private store: Store) {
        super();
    }

    ngOnInit(): void {
        this.store.dispatch(dashboardActions.loadDashboards());

        this.store.select(selectDashboards)
            .pipe(this.untilDestroyed)
            .subscribe(dashboards => this.updateDashboards(dashboards));
    }

    public openCreateSidebar(): void {
        this.openSidebar(SidebarType.Create);
    }

    public openDisplaySidebar(): void {
        this.openSidebar(SidebarType.Display);
    }

    public selectDashboard(selection: MatSelectChange): void {
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

    public goToDate(event: MatDatepickerInputEvent<Date>): void {
        this.currentDate = addHours(event.value!, 12);
    }

    private openSidebar(type: SidebarType): void {
        this.sidebarType = type;
        this.sidebarOpen = true;
    }

    private updateDashboards(dashboards: Dashboard[]): void {
        const newDashboards = dashboards.filter(d => !this.dashboards.includes(d));
        this.dashboards = dashboards;
        if (dashboards.length > 0) {
            let selectedDashboardIndex = 0;
            if (newDashboards.length === 1) {
                selectedDashboardIndex = this.dashboards.indexOf(newDashboards[0]);
            }
            this.currentDashboard = dashboards[selectedDashboardIndex];
            this.store.dispatch(dashboardActions.chooseDashboard({ dashboardId: this.currentDashboard.id }));
        }
    }
}

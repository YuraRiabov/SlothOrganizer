import * as dashboardActions from '@store/actions/dashboard.actions';
import * as taskActions from '@store/actions/task.actions';

import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { selectChosenDashboard, selectDashboards, selectSidebarType } from '@store/selectors/dashboard.selectors';
import { selectTaskToUpdate, selectTasks } from '@store/selectors/task.selectors';

import { BaseComponent } from '@shared/components/base/base.component';
import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';
import { Store } from '@ngrx/store';
import { Task } from '#types/dashboard/tasks/task';
import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { TimelineScale } from '#types/dashboard/timeline/enums/timeline-scale';
import { addHours } from 'date-fns';
import { selectHasCalendar } from '@store/selectors/auth.selectors';

@Component({
    selector: 'so-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent extends BaseComponent implements OnInit {
    public readonly SidebarType = SidebarType;
    public currentDate: Date = new Date();
    public timelineScale = TimelineScale.Day;

    public dashboards$?: Observable<Dashboard[]>;
    public currentDashboard$?: Observable<Dashboard>;

    public tasks$?: Observable<Task[]>;
    public taskToUpdate$?: Observable<NewTask>;

    public hasCalendar$ = this.store.select(selectHasCalendar);

    public sidebarType$: Observable<SidebarType> = of(SidebarType.None);

    public creatingNewDashboard: boolean = false;

    constructor(private store: Store) {
        super();
    }

    ngOnInit(): void {
        this.store.dispatch(dashboardActions.loadDashboards());

        this.dashboards$ = this.store.select(selectDashboards);
        this.currentDashboard$ = this.store.select(selectChosenDashboard);
        this.sidebarType$ = this.store.select(selectSidebarType);
        this.tasks$ = this.store.select(selectTasks);
        this.taskToUpdate$ = this.store.select(selectTaskToUpdate);
    }

    public openCreateSidebar(): void {
        this.store.dispatch(dashboardActions.openSidebar({ sidebarType: SidebarType.Create }));
    }

    public displayTask(taskBlock: TaskBlock): void {
        this.store.dispatch(taskActions.chooseTask({ taskBlock }));
    }

    public closeSidebar(): void {
        this.store.dispatch(dashboardActions.closeSidebar());
    }

    public selectDashboard(dashboard: Dashboard): void {
        this.store.dispatch(dashboardActions.chooseDashboard({ dashboardId: dashboard.id }));
    }

    public createDashboard(title: string): void {
        this.store.dispatch(dashboardActions.createDashbaord({ title }));
    }

    public updateTask(task: NewTask): void {
        this.store.dispatch(taskActions.editTask({ task }));
    }

    public createTask(newTask: NewTask) {
        this.store.dispatch(taskActions.createTask({ newTask }));
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

    public exportDashboard(): void {
        this.store.dispatch(dashboardActions.exportDashboard());
    }
}

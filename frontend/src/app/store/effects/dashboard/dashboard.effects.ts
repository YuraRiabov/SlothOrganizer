import * as dashboardActions from '@store/actions/dashboard.actions';
import * as taskActions from '@store/actions/task.actions';

import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap } from 'rxjs';

import { DashboardService } from '@api/dashboard.service';
import { Injectable } from '@angular/core';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';

@Injectable()
export class DashboardEffects {
    public getDashboards$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.loadDashboards),
                mergeMap(() => this.dashboardService.retrieve()),
                map((dashboards) => dashboardActions.loadDashboardsSuccess({ dashboards }))
            );
        }
    );

    public addDashboard$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.createDashbaord),
                mergeMap((action) => this.dashboardService.create({ title: action.title })),
                map(dashboard => dashboardActions.dashboardCreated({ dashboard }))
            );
        }
    );

    public changeDashboard$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.chooseDashboard, dashboardActions.dashboardCreated),
                map(() => taskActions.loadTasks())
            );
        }
    );

    public openDisplaySidebar$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(taskActions.chooseTask),
                map(() => dashboardActions.openSidebar({ sidebarType: SidebarType.Display }))
            );
        }
    );

    public closeDisplaySidebar$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(taskActions.taskCreated, taskActions.taskEdited, taskActions.deleteTask),
                map(() => dashboardActions.closeSidebar())
            );
        }
    );

    constructor(private dashboardService: DashboardService, private actions$: Actions) {}
}
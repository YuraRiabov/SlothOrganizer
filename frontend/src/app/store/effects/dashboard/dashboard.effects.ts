import * as dashboardActions from '@store/actions/dashboard.actions';

import { Actions, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap } from 'rxjs';

import { DashboardService } from '@api/dashboard.service';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectUserId } from '@store/selectors/auth.selectors';

@Injectable()
export class DashboardEffects {
    public getDashboards$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.loadDashboards),
                concatLatestFrom(() => this.store.select(selectUserId)),
                mergeMap(([action, id]) => this.dashboardService.find(id)),
                map((dashboards) => dashboardActions.loadDashboardsSuccess({ dashboards }))
            );
        }
    );

    public addDashboard$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.createDashbaord),
                mergeMap(action => this.dashboardService.create(action.dashboard)),
                map(dashboard => dashboardActions.dashboardCreated({ dashboard }))
            );
        }
    );

    public changeDashboard$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.chooseDashboard),
                map(() => dashboardActions.loadTasks())
            );
        }
    );

    constructor(private dashboardService: DashboardService, private actions$: Actions, private store: Store) {}
}
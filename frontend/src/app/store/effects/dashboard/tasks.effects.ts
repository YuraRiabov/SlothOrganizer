import * as dashboardActions from '@store/actions/dashboard.actions';

import { Actions, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap } from 'rxjs';

import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { TasksService } from '@api/tasks.service';
import { selectChosenDashboardId } from '@store/selectors/dashboard.selectors';

@Injectable()
export class TasksEffects {
    public addTask$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.createTask),
                mergeMap((action) => this.tasksService.create(action.newTask)),
                map((task) => dashboardActions.taskCreated({ task }))
            );
        }
    );

    public loadTasks$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.loadTasks),
                concatLatestFrom(() => this.store.select(selectChosenDashboardId)),
                mergeMap(([action, id]) => this.tasksService.load(id)),
                map((tasks) => dashboardActions.tasksLoaded({ tasks }))
            );
        }
    );

    constructor(private tasksService: TasksService, private actions$: Actions, private store: Store) { }
}
import * as dashboardActions from '@store/actions/dashboard.actions';

import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap } from 'rxjs';

import { Injectable } from '@angular/core';
import { TasksService } from '@api/tasks.service';

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

    constructor(private tasksService: TasksService, private actions$: Actions) {}
}
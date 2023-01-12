import * as dashboardActions from '@store/actions/dashboard.actions';

import { Actions, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap, switchMap } from 'rxjs';
import { selectChosenTaskCompletion, selectChosenTaskCompletionId } from '@store/selectors/dashboard.selectors';

import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { TaskComletionService } from '@api/task-completion.service';

@Injectable()
export class TaskCompletionEffects {
    public markCompleted$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.markTaskCompleted),
                concatLatestFrom(() => this.store.select(selectChosenTaskCompletion)),
                mergeMap(([, taskCompletion]) => this.taskCompletionService.update({...taskCompletion, isSuccessful: true})),
                map((taskCompletion) => dashboardActions.taskMarkedCompleted({ taskCompletion }))
            );
        }
    );

    public delete$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.deleteTask),
                concatLatestFrom(() => this.store.select(selectChosenTaskCompletionId)),
                mergeMap(([, id]) => this.taskCompletionService.remove(id))
            );
        }, {
            dispatch: false
        }
    );

    constructor(private taskCompletionService: TaskComletionService, private actions$: Actions, private store: Store) { }
}
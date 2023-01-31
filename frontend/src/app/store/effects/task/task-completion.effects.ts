import * as taskActions from '@store/actions/task.actions';

import { Actions, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap, take } from 'rxjs';
import { selectChosenTaskCompletion, selectChosenTaskCompletionId } from '@store/selectors/task.selectors';

import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { TaskComletionService } from '@api/task-completion.service';

@Injectable()
export class TaskCompletionEffects {
    public markCompleted$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(taskActions.markCompleted),
                concatLatestFrom(() => this.store.select(selectChosenTaskCompletion)),
                mergeMap(([, taskCompletion]) => this.taskCompletionService.update({...taskCompletion, isSuccessful: true}).pipe(take(1))),
                map((taskCompletion) => taskActions.markCompletedSuccess({ taskCompletion }))
            );
        }
    );

    public delete$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(taskActions.deleteTask),
                concatLatestFrom(() => this.store.select(selectChosenTaskCompletionId)),
                mergeMap(([, id]) => this.taskCompletionService.remove(id).pipe(take(1)))
            );
        },
        {
            dispatch: false
        }
    );

    constructor(private taskCompletionService: TaskComletionService, private actions$: Actions, private store: Store) { }
}
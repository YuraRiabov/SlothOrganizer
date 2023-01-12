import * as dashboardActions from '@store/actions/dashboard.actions';

import { Actions, act, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap } from 'rxjs';
import { selectChosenDashboardId, selectChosenTaskBlock } from '@store/selectors/dashboard.selectors';

import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
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

    public update$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.editTask),
                concatLatestFrom(() => this.store.select(selectChosenTaskBlock)),
                mergeMap(([action, taskBlock]) => this.tasksService.update({
                    task: {
                        ...taskBlock.task,
                        title: action.task.title,
                        description: action.task.description
                    },
                    taskCompletion: {
                        ...taskBlock.taskCompletion,
                        start: action.task.start,
                        end: action.task.end,
                    },
                    endRepeating: action.task.endRepeating
                })),
                map((task) => dashboardActions.taskEdited({ task }))
            );
        }
    );

    constructor(private tasksService: TasksService, private actions$: Actions, private store: Store) { }
}
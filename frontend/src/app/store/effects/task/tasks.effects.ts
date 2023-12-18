import * as taskActions from '@store/actions/task.actions';

import { Actions, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import { exportDashboard, loadDashboards } from '@store/actions/dashboard.actions';
import { map, mergeMap, switchMap, take } from 'rxjs';

import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { TasksService } from '@api/tasks.service';
import { selectChosenDashboardId } from '@store/selectors/dashboard.selectors';
import { selectChosenTaskBlock } from '@store/selectors/task.selectors';

@Injectable()
export class TasksEffects {
    public addTask$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(taskActions.createTask),
                concatLatestFrom(() => this.store.select(selectChosenDashboardId)),
                mergeMap(([action, id]) => this.tasksService.create({ ...action.newTask, dashboardId: id}).pipe(take(1))),
                map((task) => taskActions.createTaskSuccess({ task }))
            );
        }
    );

    public loadTasks$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(taskActions.loadTasks),
                concatLatestFrom(() => this.store.select(selectChosenDashboardId)),
                mergeMap(([, id]) => this.tasksService.load(id).pipe(take(1))),
                map((tasks) => taskActions.loadTasksSuccess({ tasks }))
            );
        }
    );

    public update$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(taskActions.editTask),
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
                    endRepeating: action.task.endRepeating,
                    shouldExport: action.task.shouldExport
                }).pipe(take(1))),
                map((task) => taskActions.editTaskSuccess({ task }))
            );
        }
    );

    public export$ = createEffect(() =>
    { return this.actions$.pipe(
        ofType(exportDashboard),
        concatLatestFrom(() => this.store.select(selectChosenDashboardId)),
        switchMap(([, id]) => this.tasksService.export(id)),
        map(() => loadDashboards())
    ); }
    );

    constructor(private tasksService: TasksService, private actions$: Actions, private store: Store) { }
}
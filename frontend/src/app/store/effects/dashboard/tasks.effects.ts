import * as dashboardActions from '@store/actions/dashboard.actions';

import { Actions, act, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap } from 'rxjs';
import { selectChosenDashboardId, selectChosenTaskBlock } from '@store/selectors/dashboard.selectors';

import { Injectable } from '@angular/core';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';
import { Store } from '@ngrx/store';
import { TasksService } from '@api/tasks.service';

@Injectable()
export class TasksEffects {
    public addTask$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.createTask),
                concatLatestFrom(() => this.store.select(selectChosenDashboardId)),
                mergeMap(([action, id]) => this.tasksService.create({ ...action.newTask, dashboardId: id})),
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

    public openDisplaySidebar$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.chooseTask),
                map(() => dashboardActions.openSidebar({ sidebarType: SidebarType.Display }))
            );
        }
    );

    public closeDisplaySidebar$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(dashboardActions.taskCreated, dashboardActions.taskEdited),
                map(() => dashboardActions.closeSidebar())
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
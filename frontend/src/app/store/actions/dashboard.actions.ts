import { createAction, props } from '@ngrx/store';

import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { NewDashboard } from '#types/dashboard/dashboard/new-dashboard';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { Task } from '#types/dashboard/tasks/task';
import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { TaskCompletion } from '#types/dashboard/tasks/task-completion';

export const loadDashboards = createAction(
    '[Dashboard page] Load dashboards'
);

export const loadDashboardsSuccess = createAction(
    '[API] Load dashboards success',
    props<{ dashboards: Dashboard[] }>()
);

export const createDashbaord = createAction(
    '[Dashboard page] Create dashboard',
    props<{ dashboard: NewDashboard }>()
);

export const dashboardCreated = createAction(
    '[API] Dashboard created',
    props<{ dashboard: Dashboard }>()
);

export const chooseDashboard = createAction(
    '[Dashboard page] Select dashboard',
    props<{ dashboardId: number }>()
);

export const createTask = createAction(
    '[Timeline] Create task',
    props<{ newTask: NewTask }>()
);

export const taskCreated = createAction(
    '[API] Task created',
    props<{ task: Task }>()
);

export const loadTasks = createAction(
    '[Dashboard page] Load tasks'
);

export const tasksLoaded = createAction(
    '[API] Tasks loaded',
    props<{ tasks: Task[] }>()
);

export const chooseTask = createAction(
    '[Timeline] Choose task',
    props<{ taskBlock: TaskBlock }>()
);

export const markTaskCompleted = createAction(
    '[Dashboard sidebar] Mark task completed'
);

export const taskMarkedCompleted = createAction(
    '[API] Task marked completed',
    props<{ taskCompletion: TaskCompletion }>()
);

export const editTask = createAction(
    '[Dasboard sidebar] Edit task',
    props<{ task: NewTask }>()
);

export const taskEdited = createAction(
    '[API] Task edited',
    props<{ task: Task }>()
);

export const deleteTask = createAction(
    '[Dashboard sidebar] Delete task'
);
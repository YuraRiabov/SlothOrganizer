import { createAction, props } from '@ngrx/store';

import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { NewDashboard } from '#types/dashboard/dashboard/new-dashboard';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { Task } from '#types/dashboard/tasks/task';

export const loadDashboards = createAction(
    '[Dashboard page] Load dashboards'
);

export const loadDashboardsSuccess = createAction(
    '[API] Load dashboards success',
    props<{ dashboards: Dashboard[]}>()
);

export const createDashbaord = createAction(
    '[Dashboard page] Create dashboard',
    props<{ title: string }>()
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
    props<{ task: Task}>()
);
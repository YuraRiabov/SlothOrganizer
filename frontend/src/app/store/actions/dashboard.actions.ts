import { createAction, props } from '@ngrx/store';

import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';
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

export const openSidebar = createAction(
    '[Dashboard page] Open sidebar',
    props<{ sidebarType: SidebarType }>()
);

export const closeSidebar = createAction(
    '[Dashboard page] Close sidebar'
);
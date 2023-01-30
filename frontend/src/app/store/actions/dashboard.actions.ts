import { createAction, props } from '@ngrx/store';

import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';

export const loadDashboards = createAction(
    '[Dashboard page] Load dashboards'
);

export const loadDashboardsSuccess = createAction(
    '[Dashboard page] Load dashboards success',
    props<{ dashboards: Dashboard[] }>()
);

export const createDashbaord = createAction(
    '[Dashboard page] Create dashboard',
    props<{ title: string }>()
);

export const createDashboardSuccess = createAction(
    '[Dashboard page] Create dashboard success',
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
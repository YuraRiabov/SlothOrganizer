import { chooseDashboard, dashboardCreated, loadDashboardsSuccess, taskCreated, tasksLoaded } from '@store/actions/dashboard.actions';
import { createReducer, on } from '@ngrx/store';

import { DashboardState } from '@store/states/dashboard-state';

const initialState: DashboardState = {
    chosenDashboardId: -1,
    dashboards: [],
    tasks: []
};

export const dashboardReducer = createReducer(
    initialState,
    on(loadDashboardsSuccess, (state, { dashboards }): DashboardState => ({ ...state, dashboards })),
    on(dashboardCreated, (state, { dashboard }): DashboardState => ({ ...state, dashboards: state.dashboards.concat(dashboard) })),
    on(chooseDashboard, (state, { dashboardId }): DashboardState => ({ ...state, chosenDashboardId: dashboardId })),
    on(taskCreated, (state, { task }): DashboardState => ({ ...state, tasks: state.tasks.concat(task) })),
    on(tasksLoaded, (state, { tasks }): DashboardState => ({ ...state, tasks }))
);
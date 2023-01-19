import { chooseDashboard, dashboardCreated, loadDashboardsSuccess, taskCreated } from '@store/actions/dashboard.actions';
import { createReducer, on } from '@ngrx/store';

import { DashboardState } from '@store/states/dashboard-state';

const initialState: DashboardState = {
    chosenDashboardId: -1,
    dashboards: [],
    tasks: []
};

export const dashboardReducer = createReducer(
    initialState,
    on(loadDashboardsSuccess, (state, { dashboards }): DashboardState => ({
        ...state,
        dashboards,
        chosenDashboardId: state.chosenDashboardId === -1 ? dashboards[0].id : state.chosenDashboardId
    })),
    on(dashboardCreated, (state, { dashboard }): DashboardState => ({
        ...state,
        dashboards: state.dashboards.concat(dashboard),
        chosenDashboardId: dashboard.id
    })),
    on(chooseDashboard, (state, { dashboardId }): DashboardState => ({ ...state, chosenDashboardId: dashboardId })),
    on(taskCreated, (state, { task }): DashboardState => ({ ...state, tasks: state.tasks.concat(task) }))
);
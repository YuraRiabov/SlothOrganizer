import { createFeatureSelector, createSelector } from '@ngrx/store';

import { DashboardState } from '@store/states/dashboard-state';

export const selectDashboardState = createFeatureSelector<DashboardState>('Dashboard');

export const selectDashboards = createSelector(
    selectDashboardState,
    (state) => state.dashboards
);

export const selectChosenDashboardId = createSelector(
    selectDashboardState,
    (state) => state.chosenDashboardId
);

export const selectTasks = createSelector(
    selectDashboardState,
    (state) => state.tasks
);
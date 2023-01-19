import { createFeatureSelector, createSelector } from '@ngrx/store';

import { DashboardState } from '@store/states/dashboard-state';
import { getDefaultDashboard } from '@utils/creation-functions/dashboard-creation.helper';

export const selectDashboardState = createFeatureSelector<DashboardState>('dashboard');

export const selectDashboards = createSelector(
    selectDashboardState,
    (state) => state.dashboards
);

export const selectChosenDashboardId = createSelector(
    selectDashboardState,
    (state) => state.chosenDashboardId
);

export const selectChosenDashboard = createSelector(
    selectDashboardState,
    (state) => state.dashboards.find(d => d.id === state.chosenDashboardId) ?? getDefaultDashboard()
);

export const selectTasks = createSelector(
    selectDashboardState,
    (state) => state.tasks
);

export const selectSidebarType = createSelector(
    selectDashboardState,
    (state) => state.sidebarType
);

export const selectChosenTaskBlock = createSelector(
    selectDashboardState,
    (state) => state.chosenTaskBlock
);

export const selectChosenTaskCompletion = createSelector(
    selectChosenTaskBlock,
    (taskBlock) => taskBlock.taskCompletion
);

export const selectChosenTaskCompletionId = createSelector(
    selectChosenTaskCompletion,
    (taskCompletion) => taskCompletion.id
);

export const selectChosenTask = createSelector(
    selectChosenTaskBlock,
    (taskBlock) => taskBlock.task
);
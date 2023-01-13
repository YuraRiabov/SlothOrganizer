import { createFeatureSelector, createSelector } from '@ngrx/store';

import { DashboardState } from '@store/states/dashboard-state';

export const selectDashboardState = createFeatureSelector<DashboardState>('dashboard');

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
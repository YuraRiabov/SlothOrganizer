import { createFeatureSelector, createSelector } from '@ngrx/store';

import { NewTask } from '#types/dashboard/tasks/new-task';
import { TaskRepeatingPeriod } from '#types/dashboard/tasks/enums/task-repeating-period';
import { TaskState } from '@store/states/task-state';

export const selectDashboardState = createFeatureSelector<TaskState>('task');

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

export const selectTaskToUpdate = createSelector(
    selectChosenTaskBlock,
    (taskBlock): NewTask => ({
        dashboardId: taskBlock.task.dashboardId,
        title: taskBlock.task.title,
        description: taskBlock.task.description,
        start: taskBlock.taskCompletion.start,
        end: taskBlock.taskCompletion.end,
        repeatingPeriod: TaskRepeatingPeriod.None,
        endRepeating: taskBlock.task.taskCompletions.length > 1
            ? taskBlock.task.taskCompletions.map(task => task.end).sort()[taskBlock.task.taskCompletions.length - 1]
            : undefined
    })
);
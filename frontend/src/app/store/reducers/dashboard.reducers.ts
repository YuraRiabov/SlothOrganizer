import { chooseDashboard, chooseTask, dashboardCreated, deleteTask, loadDashboardsSuccess, taskCreated, taskEdited, taskMarkedCompleted, tasksLoaded } from '@store/actions/dashboard.actions';
import { createReducer, on } from '@ngrx/store';

import { DashboardState } from '@store/states/dashboard-state';
import { TaskBlock } from '#types/dashboard/timeline/task-block';

const initialState: DashboardState = {
    chosenDashboardId: -1,
    dashboards: [],
    tasks: [],
    chosenTaskBlock: {} as TaskBlock
};

export const dashboardReducer = createReducer(
    initialState,
    on(loadDashboardsSuccess, (state, { dashboards }): DashboardState => ({ ...state, dashboards })),
    on(dashboardCreated, (state, { dashboard }): DashboardState => ({ ...state, dashboards: state.dashboards.concat(dashboard) })),
    on(chooseDashboard, (state, { dashboardId }): DashboardState => ({ ...state, chosenDashboardId: dashboardId })),
    on(taskCreated, (state, { task }): DashboardState => ({ ...state, tasks: state.tasks.concat(task) })),
    on(tasksLoaded, (state, { tasks }): DashboardState => ({ ...state, tasks })),
    on(chooseTask, (state, { taskBlock }): DashboardState => ({ ...state, chosenTaskBlock: taskBlock })),
    on(deleteTask, (state) => ({
        ...state,
        tasks: state.tasks.map(
            task => task.id !== state.chosenTaskBlock.task.id
                ? task
                : {
                    ...task,
                    taskCompletions: task.taskCompletions.filter(
                        taskCompletion => taskCompletion.id !== state.chosenTaskBlock.taskCompletion.id
                    )
                }
        )
    })),
    on(taskMarkedCompleted, (state, { taskCompletion }) => ({
        ...state,
        tasks: state.tasks.map(
            task => task.id !== taskCompletion.taskId
                ? task
                : {
                    ...task,
                    taskCompletions: task.taskCompletions.map(
                        oldCompletion => oldCompletion.id !== taskCompletion.id
                            ? oldCompletion
                            : taskCompletion
                    )
                }
        )
    })),
    on(taskEdited, (state, { task }) => ({
        ...state,
        tasks: state.tasks.map(
            oldTask => oldTask.id !== task.id
                ? oldTask
                : task
        )
    }))
);
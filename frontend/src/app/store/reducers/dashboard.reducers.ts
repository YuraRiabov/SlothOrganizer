import { chooseDashboard, chooseTask, closeSidebar, dashboardCreated, deleteTask, loadDashboardsSuccess, openSidebar, taskCreated, taskEdited, taskMarkedCompleted, tasksLoaded } from '@store/actions/dashboard.actions';
import { createReducer, on } from '@ngrx/store';

import { DashboardState } from '@store/states/dashboard-state';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';
import { TaskBlock } from '#types/dashboard/timeline/task-block';

const initialState: DashboardState = {
    sidebarType: SidebarType.None,
    chosenDashboardId: -1,
    dashboards: [],
    tasks: [],
    chosenTaskBlock: {} as TaskBlock
};

export const dashboardReducer = createReducer(
    initialState,
    on(tasksLoaded, (state, { tasks }): DashboardState => ({ ...state, tasks })),
    on(chooseTask, (state, { taskBlock }): DashboardState => ({ ...state, chosenTaskBlock: taskBlock })),
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
    on(taskCreated, (state, { task }): DashboardState => ({ ...state, tasks: state.tasks.concat(task) })),
    on(openSidebar, (state, { sidebarType }): DashboardState => ({ ...state, sidebarType })),
    on(closeSidebar, (state): DashboardState => ({ ...state, sidebarType: SidebarType.None })),
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
        ),
        chosenTaskBlock: {
            ...state.chosenTaskBlock,
            taskCompletion: {
                ...state.chosenTaskBlock.taskCompletion,
                isSuccessful: taskCompletion.isSuccessful
            }
        }
    })),
    on(taskEdited, (state, { task }) => ({
        ...state,
        tasks: state.tasks.map(
            oldTask => oldTask.id !== task.id
                ? oldTask
                : task
        ),
        chosenTaskBlock: {
            ...state.chosenTaskBlock,
            task,
            taskCompletion: task.taskCompletions.find(taskCompletion => taskCompletion.id === state.chosenTaskBlock.taskCompletion.id)!
        }
    }))
);
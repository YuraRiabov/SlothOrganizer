import { chooseTask, createTaskSuccess, deleteTask, editTaskSuccess, exportTask, loadTasksSuccess, markCompletedSuccess } from '@store/actions/task.actions';
import { createReducer, on } from '@ngrx/store';

import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { TaskState } from '@store/states/task-state';

const initialState: TaskState = {
    tasks: [],
    chosenTaskBlock: {} as TaskBlock
};

export const taskReducer = createReducer(
    initialState,
    on(loadTasksSuccess, (state, { tasks }): TaskState => ({ ...state, tasks })),
    on(chooseTask, (state, { taskBlock }): TaskState => ({ ...state, chosenTaskBlock: taskBlock })),
    on(createTaskSuccess, (state, { task }): TaskState => ({ ...state, tasks: state.tasks.concat(task) })),
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
    on(exportTask, (state) => ({
        ...state,
        tasks: state.tasks.map(
            task => task.id !== state.chosenTaskBlock.task.id
                ? task
                : {
                    ...task,
                    taskCompletions: task.taskCompletions.map(
                        taskCompletion => taskCompletion.id !== state.chosenTaskBlock.taskCompletion.id
                            ? taskCompletion
                            : {
                                ...taskCompletion,
                                isExported: true
                            }
                    )
                }
        ),
        chosenTaskBlock: {
            ...state.chosenTaskBlock,
            taskCompletion: {
                ...state.chosenTaskBlock.taskCompletion,
                isExported: true
            }
        }
    })),
    on(markCompletedSuccess, (state, { taskCompletion }) => ({
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
    on(editTaskSuccess, (state, { task }) => ({
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
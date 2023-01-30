import { createAction, props } from '@ngrx/store';

import { NewTask } from '#types/dashboard/tasks/new-task';
import { Task } from '#types/dashboard/tasks/task';
import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { TaskCompletion } from '#types/dashboard/tasks/task-completion';

export const createTask = createAction(
    '[Timeline] Create task',
    props<{ newTask: NewTask }>()
);

export const createTaskSuccess = createAction(
    '[Timeline] Create task success',
    props<{ task: Task }>()
);

export const loadTasks = createAction(
    '[Dashboard page] Load tasks'
);

export const loadTasksSuccess = createAction(
    '[Dashboard page] Load tasks success',
    props<{ tasks: Task[] }>()
);

export const chooseTask = createAction(
    '[Timeline] Choose task',
    props<{ taskBlock: TaskBlock }>()
);

export const markCompleted = createAction(
    '[Task info] Mark completed'
);

export const markCompletedSuccess = createAction(
    '[Task info] Mark completed success',
    props<{ taskCompletion: TaskCompletion }>()
);

export const editTask = createAction(
    '[Task info] Edit task',
    props<{ task: NewTask }>()
);

export const editTaskSuccess = createAction(
    '[Task info] Edit task success',
    props<{ task: Task }>()
);

export const deleteTask = createAction(
    '[Task info] Delete task'
);

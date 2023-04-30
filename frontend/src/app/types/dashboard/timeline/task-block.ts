import { Task } from '../tasks/task';
import { TaskCompletion } from '../tasks/task-completion';
import { TaskStatus } from './enums/task-status';

export interface TaskBlock {
    startColumn?: number;
    endColumn?: number;
    task: Task;
    taskCompletion: TaskCompletion;
    status: TaskStatus;
}
import { Task } from '../tasks/task';
import { TaskCompletion } from '../tasks/task-completion';

export interface TaskBlock {
    startColumn?: number;
    endColumn?: number;
    task: Task;
    taskCompletion: TaskCompletion;
    color: string;
}
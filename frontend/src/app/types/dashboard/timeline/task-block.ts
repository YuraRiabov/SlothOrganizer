import { Task } from '../tasks/task';
import { TaskCompletion } from '../tasks/task-completion';

export interface TaskBlock {
    task: Task;
    taskCompletion: TaskCompletion;
    color: string;
}
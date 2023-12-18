import { Task } from './task';
import { TaskCompletion } from './task-completion';

export interface TaskUpdate {
    task: Task;
    taskCompletion: TaskCompletion;
    endRepeating?: Date;
    shouldExport: boolean;
}
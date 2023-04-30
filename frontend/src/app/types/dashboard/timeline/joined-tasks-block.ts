import { TaskBlock } from './task-block';

export interface JoinedTasksBlock {
    tasks: TaskBlock[];
    startColumn: number;
    endColumn: number;
}
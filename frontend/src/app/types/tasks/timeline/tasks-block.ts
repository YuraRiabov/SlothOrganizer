import { Task } from '../task';

export interface TasksBlock {
    tasks: Task[];
    startColumn: number;
    endColumn: number;
    expanded: boolean;
}
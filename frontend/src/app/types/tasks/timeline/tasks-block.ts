import { Task } from '../task';

export interface TasksBlock {
    tasks: Task[];
    expanded: boolean;
}
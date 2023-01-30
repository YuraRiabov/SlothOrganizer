import { Task } from '#types/dashboard/tasks/task';
import { TaskBlock } from '#types/dashboard/timeline/task-block';

export interface TaskState {
    tasks: Task[];
    chosenTaskBlock: TaskBlock;
}
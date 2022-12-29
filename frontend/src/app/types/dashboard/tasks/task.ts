import { TaskCompletion } from './task-completion';

export interface Task {
    id: number;
    dashboardId: number;
    title: string;
    description: string;
    taskCompletions: TaskCompletion[];
}
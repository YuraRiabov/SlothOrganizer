import { TaskRepeatingPeriod } from './enums/task-repeating-period';

export interface NewTask {
    dashboardId?: number;
    title: string;
    description: string;
    start: Date;
    end: Date;
    repeatingPeriod: TaskRepeatingPeriod;
    endRepeating?: Date;
    shouldExport: boolean;
}
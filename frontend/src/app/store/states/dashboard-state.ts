import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { Task } from '#types/dashboard/tasks/task';

export interface DashboardState {
    selectedDashboardId: number;
    dashboards: Dashboard[];
    tasks: Task[];
}
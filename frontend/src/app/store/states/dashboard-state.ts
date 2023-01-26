import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';
import { Task } from '#types/dashboard/tasks/task';
import { TaskBlock } from '#types/dashboard/timeline/task-block';

export interface DashboardState {
    chosenDashboardId: number;
    dashboards: Dashboard[];
    tasks: Task[];
    sidebarType: SidebarType;
    chosenTaskBlock: TaskBlock;
}
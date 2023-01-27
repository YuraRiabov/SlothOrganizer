import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';

export interface DashboardState {
    chosenDashboardId: number;
    dashboards: Dashboard[];
    sidebarType: SidebarType;
}
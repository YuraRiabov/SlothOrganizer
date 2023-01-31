import { RootState } from '@store/states/root-state';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';
import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { Token } from '#types/auth/token';
import { User } from '#types/user/user';

export const getEmptyState = (): RootState => ({
    authState: {
        user: {} as User,
        token: {} as Token,
        invalidPassword: false
    },
    dashboard: {
        sidebarType: SidebarType.None,
        chosenDashboardId: -1,
        dashboards: []
    },
    task: {
        tasks: [],
        chosenTaskBlock: {} as TaskBlock
    }
});
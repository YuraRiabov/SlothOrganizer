import { AuthState } from './auth-state';
import { DashboardState } from './dashboard-state';
import { TaskState } from './task-state';

export interface RootState {
    authState: AuthState,
    dashboard: DashboardState,
    task: TaskState
}
import { chooseDashboard, closeSidebar, dashboardCreated, loadDashboardsSuccess, openSidebar } from '@store/actions/dashboard.actions';
import { createReducer, on } from '@ngrx/store';

import { DashboardState } from '@store/states/dashboard-state';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';

const initialState: DashboardState = {
    sidebarType: SidebarType.None,
    chosenDashboardId: -1,
    dashboards: []
};

export const dashboardReducer = createReducer(
    initialState,
    on(loadDashboardsSuccess, (state, { dashboards }): DashboardState => ({
        ...state,
        dashboards,
        chosenDashboardId: state.chosenDashboardId === -1 ? dashboards[0].id : state.chosenDashboardId
    })),
    on(dashboardCreated, (state, { dashboard }): DashboardState => ({
        ...state,
        dashboards: state.dashboards.concat(dashboard),
        chosenDashboardId: dashboard.id
    })),
    on(chooseDashboard, (state, { dashboardId }): DashboardState => ({ ...state, chosenDashboardId: dashboardId })),
    on(openSidebar, (state, { sidebarType }): DashboardState => ({ ...state, sidebarType })),
    on(closeSidebar, (state): DashboardState => ({ ...state, sidebarType: SidebarType.None }))
);
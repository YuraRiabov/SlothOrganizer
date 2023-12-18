import { createFeatureSelector, createSelector } from '@ngrx/store';

import { AuthState } from '../states/auth-state';
import { Token } from 'src/app/types/auth/token';
import { User } from 'src/app/types/user/user';
import { getDefaultToken } from '@utils/creation-functions/token-creation.helper';

export const selectAuthState = createFeatureSelector<AuthState>('authState');

export const selectUser = createSelector(
    selectAuthState,
    (state) => state.user
);

export const selectToken = createSelector(
    selectAuthState,
    (state) => state?.token ?? getDefaultToken()
);

export const selectUserId = createSelector(
    selectUser,
    (user: User) => user.id
);

export const selectUserEmail = createSelector(
    selectUser,
    (user: User) => user.email
);

export const selectAccessToken = createSelector(
    selectToken,
    (token: Token) => token.accessToken
);

export const selectInvalidPassword = createSelector(
    selectAuthState,
    (state) => state.invalidPassword
);

export const selectAvatarUrl = createSelector(
    selectUser,
    (user: User) => user.avatarUrl
);

export const selectCalendar = createSelector(
    selectUser,
    user => user.calendar
);

export const selectCalendarId = createSelector(
    selectCalendar,
    calendar => calendar != null ? calendar.id : null
);

export const selectConnectedCalendar = createSelector(
    selectCalendar,
    calendar => calendar?.connectedCalendar
);

export const selectHasCalendar = createSelector(
    selectConnectedCalendar,
    calendar => calendar != null
)
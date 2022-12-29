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

export const selectAccessToken = createSelector(
    selectToken,
    (token: Token) => token.accessToken
);

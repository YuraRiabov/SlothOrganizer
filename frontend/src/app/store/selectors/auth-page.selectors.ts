import { createFeatureSelector, createSelector } from '@ngrx/store';

import { AuthState } from 'src/app/types/states/authState';
import { Token } from 'src/app/types/auth/token';
import { User } from 'src/app/types/user/user';

export const selectAuthState = createFeatureSelector<AuthState>('authState');

export const selectUser = createSelector(
  selectAuthState,
  (state) => state.user
);

export const selectToken = createSelector(
  selectAuthState,
  (state) => state.token
);

export const selectUserId = createSelector(
  selectUser,
  (state: User) => state.id
);

export const selectAccessToken = createSelector(
  selectToken,
  (state: Token) => state.accessToken
);

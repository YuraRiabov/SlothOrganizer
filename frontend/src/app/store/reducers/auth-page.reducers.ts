import { createReducer, on } from '@ngrx/store';
import { register, verifyEmail } from '../actions/login-page.actions';

import { AuthState } from 'src/app/types/states/authState';
import { Token } from 'src/app/types/auth/token';
import { User } from 'src/app/types/user/user';

export const initialState: AuthState = {
  user: {} as User,
  token: {} as Token
};

export const authPageReducer = createReducer(
  initialState,
  on(register, (state, { user }): AuthState => ({ ...state, user })),
  on(verifyEmail, (state, { token }): AuthState => ({ ...state, token }))
);

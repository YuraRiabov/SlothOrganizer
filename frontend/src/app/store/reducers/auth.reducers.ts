import { addEmail, addToken, addUser, login, verifyEmail } from '../actions/auth.actions';
import { createReducer, on } from '@ngrx/store';

import { AuthState } from '../states/auth-state';
import { Token } from 'src/app/types/auth/token';
import { User } from 'src/app/types/user/user';

export const initialState: AuthState = {
    user: {} as User,
    token: {} as Token
};

export const authReducer = createReducer(
    initialState,
    on(addUser, (state, { user }): AuthState => ({ ...state, user })),
    on(addToken, (state, { token }): AuthState => ({ ...state, token })),
    on(login, (state, { authState }): AuthState => ({ ...state, token: authState.token, user: authState.user })),
    on(verifyEmail, (state, { authState }): AuthState => ({ ...state, token: authState.token, user: authState.user })),
    on(addEmail, (state, { email }): AuthState => ({ ...state, user: { ...state.user, email}}))
);

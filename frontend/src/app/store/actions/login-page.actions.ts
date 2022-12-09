import { createAction, props } from '@ngrx/store';

import { AuthState } from '@store/states/auth-state';
import { Token } from '#types/auth/token';
import { User } from '#types/user/user';

export const addUser = createAction(
    '[Sign up, Sign in page] Add User',
    props<{ user: User }>()
);

export const addToken = createAction(
    '[Verify email page] Verify email',
    props<{ token: Token }>()
);

export const login = createAction(
    '[Sign in page, verify email page] Login',
    props<{authState: AuthState}>()
);

export const addEmail = createAction(
    '[Enter email page] Add email',
    props<{ email: string}>()
);

import { createAction, props } from '@ngrx/store';

import { Token } from 'src/app/types/auth/token';
import { User } from 'src/app/types/user/user';

export const register = createAction(
  '[Sign up page] Register',
  props<{ user: User }>()
);

export const verifyEmail = createAction(
  '[Verify email page] Verify email',
  props<{ token: Token }>()
);

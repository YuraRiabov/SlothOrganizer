import { createAction, props } from '@ngrx/store';

import { Token } from 'src/app/types/tokens/token';
import { User } from 'src/app/types/user/user';

export const register = createAction(
  '[Sign up page] Register',
  props<{ user: User }>()
);

export const verifyEmail = createAction(
  '[Verify email page] Verigy email',
  props<{ token: Token }>()
);

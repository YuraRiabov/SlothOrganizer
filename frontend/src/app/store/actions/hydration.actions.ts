import { createAction, props } from '@ngrx/store';

import { AuthState } from '@store/states/auth-state';

export const LOGOUT = '[App] Logout';

export const logoutAction = createAction(
    '[App] Logout'
);

export const hydrate = createAction('[Hydration] Hydrate');

export const hydrateSuccess = createAction(
    '[Hydration] Hydrate Success',
    props<{ state: AuthState }>()
);

export const hydrateFailure = createAction('[Hydration] Hydrate Failure');

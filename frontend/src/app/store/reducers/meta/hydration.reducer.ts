import * as HydrationActions from '@store/actions/hydration.actions';

import { Action, ActionReducer } from '@ngrx/store';

import { AuthState } from '@store/states/auth-state';

const isHydrateSuccess = (
    action: Action
): action is ReturnType<typeof HydrationActions.hydrateSuccess> => {
    return action.type === HydrationActions.hydrateSuccess.type;
};

export const hydrationMetaReducer = (
    reducer: ActionReducer<AuthState>
): ActionReducer<AuthState> => {
    return (state, action) => {
        if (isHydrateSuccess(action)) {
            return action.state;
        } else {
            return reducer(state, action);
        }
    };
};
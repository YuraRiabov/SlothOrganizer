import { ActionReducer } from '@ngrx/store';
import { AuthState } from '@store/states/auth-state';
import { LOGOUT } from '@store/actions/hydration.actions';
import { getEmptyState } from '@utils/creation-functions/auth-state.helper';

export const logoutMetaReducer = (
    reducer: ActionReducer<AuthState>
): ActionReducer<AuthState> => {
    return (state, action) => {
        if (action.type === LOGOUT) {
            return getEmptyState();
        }
        return reducer(state, action);
    };
};
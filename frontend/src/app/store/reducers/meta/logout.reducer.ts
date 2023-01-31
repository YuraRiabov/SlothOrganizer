import { ActionReducer } from '@ngrx/store';
import { LOGOUT } from '@store/actions/hydration.actions';
import { RootState } from '@store/states/root-state';
import { getEmptyState } from '@utils/creation-functions/auth-state.helper';

export const logoutMetaReducer = (
    reducer: ActionReducer<RootState>
): ActionReducer<RootState> => {
    return (state, action) => {
        if (action.type === LOGOUT) {
            return getEmptyState();
        }
        return reducer(state, action);
    };
};
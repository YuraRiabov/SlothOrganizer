import { AuthState } from '@store/states/auth-state';
import { User } from '#types/user/user';
import { getDefaultToken } from './token-creation.helper';

export const getEmptyState = (): AuthState => ({
    user: {} as User,
    token: getDefaultToken()
});
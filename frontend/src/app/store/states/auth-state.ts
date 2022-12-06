import { Token } from '#types/auth/token';
import { User } from '#types/user/user';

export interface AuthState {
    user: User;
    token: Token;
}

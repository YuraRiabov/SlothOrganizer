import { Token } from '#types/auth/token';
import { Calendar } from '#types/user/calendar';
import { User } from '#types/user/user';

export interface AuthState {
    user: User;
    token?: Token;
    invalidPassword?: boolean;
}

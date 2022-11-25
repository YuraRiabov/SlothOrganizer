import { Token } from '../tokens/token';
import { User } from '../user/user';

export interface AuthState {
  user: User;
  token: Token;
}

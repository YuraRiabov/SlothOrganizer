import { AuthState } from '../types/states/authState';
import { HttpInternalService } from './http-internal.service';
import { Injectable } from '@angular/core';
import { NewUser } from 'src/app/types/user/NewUser';
import { Token } from '../types/auth/token';
import { User } from '../types/user/user';
import { UserAuthorization } from '../types/auth/userAuthorization';
import { VerificationCode } from '../types/auth/verificationCode';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private baseUri: string = '/auth';

    constructor(private httpService: HttpInternalService) {}

    public signUp(user: NewUser) {
        return this.httpService.postRequest<User>(`${this.baseUri}/signup`, user);
    }

    public signIn(user: UserAuthorization) {
        return this.httpService.postRequest<AuthState>(`${this.baseUri}/signin`, user);
    }

    public verifyEmail(verificationCode: VerificationCode) {
        return this.httpService.putRequest<Token>(
            `${this.baseUri}/verifyEmail`,
            verificationCode
        );
    }

    public resendCode(userId: number) {
        return this.httpService.postRequest(
            `${this.baseUri}/resendCode/${userId}`,
            {}
        );
    }
}

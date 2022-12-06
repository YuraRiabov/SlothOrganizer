import { AuthState } from '@store/states/auth-state';
import { HttpService } from './http-internal.service';
import { Injectable } from '@angular/core';
import { NewUser } from '#types/user/new-user';
import { Observable } from 'rxjs';
import { Token } from '#types/auth/token';
import { User } from '#types/user/user';
import { UserAuthorization } from '#types/auth/userAuthorization';
import { VerificationCode } from '#types/auth/verification-code';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private baseUri: string = '/auth';

    constructor(private httpService: HttpService) {}

    public signUp(user: NewUser) : Observable<User> {
        return this.httpService.post<User>(`${this.baseUri}/signup`, user);
    }

    public signIn(user: UserAuthorization) {
        return this.httpService.post<AuthState>(`${this.baseUri}/signin`, user);
    }
    public verifyEmail(verificationCode: VerificationCode) : Observable<Token> {
        return this.httpService.put<Token>(
            `${this.baseUri}/verifyEmail`,
            verificationCode
        );
    }

    public resendCode(userId: number) : Observable<null> {
        return this.httpService.post(`${this.baseUri}/resendCode/${userId}`);
    }

    public refreshToken(token: Token) {
        return this.httpService.put<Token>(`${this.baseUri}/refreshToken`, token);
    }
}

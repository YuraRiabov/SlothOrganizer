import { AuthState } from '@store/states/auth-state';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { Login } from '#types/auth/login';
import { NewUser } from '#types/user/new-user';
import { Observable } from 'rxjs';
import { Token } from '#types/auth/token';
import { User } from '#types/user/user';
import { VerificationCode } from '#types/auth/verification-code';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private baseUri: string = '/auth';

    constructor(private httpService: HttpService) {}

    public signUp(user: NewUser) : Observable<User> {
        return this.httpService.post<User>(`${this.baseUri}/sign-up`, user);
    }

    public signIn(login: Login): Observable<AuthState> {
        return this.httpService.post<AuthState>(`${this.baseUri}/sign-in`, login);
    }
    public verifyEmail(verificationCode: VerificationCode) : Observable<Token> {
        return this.httpService.put<Token>(
            `${this.baseUri}/verify-email`,
            verificationCode
        );
    }

    public resendCode(userId: number) : Observable<null> {
        return this.httpService.post(`${this.baseUri}/resend-code/${userId}`);
    }

    public refreshToken(token: Token) : Observable<Token> {
        return this.httpService.put<Token>(`${this.baseUri}/refresh-token`, token);
    }
}

import { AuthState } from '@store/states/auth-state';
import { HttpClient } from '@angular/common/http';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { Login } from '#types/auth/login';
import { NewUser } from '#types/user/new-user';
import { Observable } from 'rxjs';
import { ResetPassword } from '#types/user/reset-password';
import { Token } from '#types/auth/token';
import { User } from '#types/user/user';
import { VerificationCode } from '#types/auth/verification-code';

@Injectable({
    providedIn: 'root'
})
export class AuthService extends HttpService {
    protected override readonly controllerUri: string = '/auth';

    constructor(http: HttpClient) {
        super(http);
    }

    public signUp(user: NewUser) : Observable<User> {
        return this.post<User>('/sign-up', user);
    }

    public signIn(login: Login): Observable<AuthState> {
        return this.post<AuthState>('/sign-in', login);
    }
    public verifyEmail(verificationCode: VerificationCode) : Observable<AuthState> {
        return this.put<AuthState>('/verify-email', verificationCode);
    }

    public resendCode(email: string) : Observable<null> {
        return this.post(`/resend-code/${email}`);
    }

    public refreshToken(token: Token) : Observable<Token> {
        return this.put<Token>('/refresh-token', token);
    }

    public sendPasswordResetLink(email: string) : Observable<null> {
        return this.post(`/send-password-reset/${email}`);
    }

    public resetPassword(resetPassword: ResetPassword) : Observable<AuthState> {
        return this.put<AuthState>('/reset-password', resetPassword);
    }
}

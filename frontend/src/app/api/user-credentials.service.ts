import { AuthState } from '@store/states/auth-state';
import { HttpClient } from '@angular/common/http';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { Login } from '#types/auth/login';
import { NewUser } from '#types/user/new-user';
import { Observable } from 'rxjs';
import { ResetPassword } from '#types/user/reset-password';
import { User } from '#types/user/user';
import { VerificationCode } from '#types/auth/verification-code';

@Injectable({
    providedIn: 'root'
})
export class UserCredentialsService extends HttpService {
    protected override readonly controllerUri: string = '/user-credentials';

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

    public resetPassword(resetPassword: ResetPassword) : Observable<AuthState> {
        return this.put<AuthState>('/reset-password', resetPassword);
    }
}

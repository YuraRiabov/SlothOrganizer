import { HttpClient } from '@angular/common/http';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Token } from '#types/auth/token';

@Injectable({
    providedIn: 'root'
})
export class AuthService extends HttpService {
    protected override readonly controllerUri: string = '/auth';

    constructor(http: HttpClient) {
        super(http);
    }

    public resendCode(email: string) : Observable<null> {
        return this.post(`/send-code/${email}`);
    }

    public refreshToken(token: Token) : Observable<Token> {
        return this.put<Token>('/refresh-token', token);
    }

    public sendPasswordResetLink(email: string) : Observable<null> {
        return this.post(`/send-password-reset/${email}`);
    }
}

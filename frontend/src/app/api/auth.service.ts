import { HttpInternalService } from './http-internal.service';
import { Injectable } from '@angular/core';
import { NewUser } from '../types/user/newUser';
import { Token } from '../types/auth/token';
import { User } from '../types/user/user';
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

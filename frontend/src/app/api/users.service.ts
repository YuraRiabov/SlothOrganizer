import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResetPassword } from '#types/user/reset-password';

@Injectable({
    providedIn: 'root'
})
export class UsersService {
    readonly baseUri = '/users';

    constructor(private http: HttpService) { }

    public resetPassword(resetPassword: ResetPassword) : Observable<null> {
        return this.http.put(`${this.baseUri}/resetPassword`, resetPassword);
    }
}

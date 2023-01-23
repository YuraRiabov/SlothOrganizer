import { HttpClient } from '@angular/common/http';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '#types/user/user';
import { UserUpdate } from '#types/user/user-update';

@Injectable({
    providedIn: 'root'
})
export class UserInfoService extends HttpService {
    protected override readonly controllerUri: string = '/users-info';

    constructor(http: HttpClient) {
        super(http);
    }

    public update(userUpdate: UserUpdate): Observable<null> {
        return this.put('', userUpdate);
    }

    public updateAvater(formData: FormData, userId: number): Observable<User> {
        return this.put<User>(`/${userId}/avatar`, formData);
    }

    public deleteAvatar(userId: number): Observable<null> {
        return this.delete(`/${userId}/avatar`);
    }
}

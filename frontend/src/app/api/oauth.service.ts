import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { HttpClient } from '@angular/common/http';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { NewDashboard } from '#types/dashboard/dashboard/new-dashboard';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class OAuthService extends HttpService {
    protected override readonly controllerUri: string = '/googleoauth';

    constructor(http: HttpClient) {
        super(http);
    }

    public redirect(): Observable<string> {
        return this.get<string>('/redirect');
    }
}

import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { HttpClient } from '@angular/common/http';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { NewDashboard } from '#types/dashboard/dashboard/new-dashboard';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class DashboardService extends HttpService {
    protected override readonly controllerUri: string = '/dashboards';

    constructor(http: HttpClient) {
        super(http);
    }

    public create(newDashboard: NewDashboard): Observable<Dashboard> {
        return this.post<Dashboard>('', newDashboard);
    }

    public find(userId: number): Observable<Dashboard[]> {
        return this.get<Dashboard[]>(`/${userId}`);
    }
}

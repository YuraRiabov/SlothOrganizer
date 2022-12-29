import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { NewDashboard } from '#types/dashboard/dashboard/new-dashboard';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class DashboardService {
    private readonly baseUri: string = '/dashboards';

    constructor(private http: HttpService) { }

    public create(newDashboard: NewDashboard): Observable<Dashboard> {
        return this.http.post<Dashboard>(`${this.baseUri}`, newDashboard);
    }

    public get(userId: number): Observable<Dashboard[]> {
        return this.http.get<Dashboard[]>(`${this.baseUri}/${userId}`);
    }
}

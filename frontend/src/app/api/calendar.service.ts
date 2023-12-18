import { HttpClient } from '@angular/common/http';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
// eslint-disable-next-line sort-imports
import { Calendar } from '#types/user/calendar';

@Injectable({
    providedIn: 'root'
})
export class CalendarService extends HttpService {
    protected override readonly controllerUri: string = '/calendar';

    constructor(http: HttpClient) {
        super(http);
    }

    public getCalendar(): Observable<Calendar> {
        return this.get<Calendar>('');
    }

    public deleteCalendar(calendarId: number): Observable<void> {
        return this.delete(`/${calendarId}`);
    }
}

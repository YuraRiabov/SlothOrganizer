import * as profileActions from '@store/actions/profile.actions';

import { Actions, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import { filter, map, switchMap, tap } from 'rxjs';

import { Injectable } from '@angular/core';
import { OAuthService } from '@api/oauth.service';
import { CalendarService } from '@api/calendar.service';
import { Calendar } from '#types/user/calendar';
import { Store } from '@ngrx/store';
import { selectCalendarId } from '@store/selectors/auth.selectors';

@Injectable()
export class CalendarEffects {
    public attachCalendar$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(profileActions.attachCalendar),
                switchMap(() => this.oauthService.redirect()),
                tap((redirectUrl) => {
                    window.location.href = redirectUrl;
                })
            );
        },
        { dispatch: false }
    );

    public getCalendar$ = createEffect(() => 
        this.actions$.pipe(
            ofType(profileActions.getCalendar),
            switchMap(() => this.calendarService.getCalendar()),
            map((calendar: Calendar | null) => profileActions.getCalendarSuccess({ calendar }))
        )
    );

    public deleteCalendar$ = createEffect(() => 
        this.actions$.pipe(
            ofType(profileActions.deleteCalendar),
            concatLatestFrom(() => this.store.select(selectCalendarId)),
            filter(([, id]) => id != null),
            switchMap(([, id]) => this.calendarService.deleteCalendar(id!)),
            map(() => profileActions.getCalendar())
        )
    );

    constructor(private oauthService: OAuthService, private calendarService: CalendarService, private actions$: Actions, private store: Store) { }
}
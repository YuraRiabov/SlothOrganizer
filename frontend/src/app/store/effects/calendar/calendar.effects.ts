import * as profileActions from '@store/actions/profile.actions';

import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, switchMap } from 'rxjs';

import { Injectable } from '@angular/core';
import { OAuthService } from '@api/oauth.service';

@Injectable()
export class CalendarEffects {
    public attachCalendar$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(profileActions.attachCalendar),
                switchMap(() => this.oauthService.redirect()),
                map((redirectUrl) => {
                    window.location.href = redirectUrl;
                })
            );
        },
        { dispatch: false }
    );

    constructor(private oauthService: OAuthService, private actions$: Actions) { }
}
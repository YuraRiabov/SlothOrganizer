import * as profileActions from '@store/actions/profile.actions';

import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap } from 'rxjs';

import { GyazoService } from '@api/gyazo.service';
import { Injectable } from '@angular/core';

@Injectable()
export class DashboardEffects {
    public uploadAvatar$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(profileActions.uploadAvatar),
                mergeMap((action) => this.gyazoService.upload(action.image)),
                map((url) => profileActions.saveAvatar({ url }))
            );
        }
    );

    constructor(private gyazoService: GyazoService, private actions$: Actions) { }
}
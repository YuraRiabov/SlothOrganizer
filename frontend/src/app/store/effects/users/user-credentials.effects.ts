import * as profileActions from '@store/actions/profile.actions';

import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap } from 'rxjs';

import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { UserCredentialsService } from '@api/user-credentials.service';

@Injectable()
export class DashboardEffects {
    public updatePassword$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(profileActions.updatePassword),
                mergeMap((action) => this.userCredentialsService.updatePassword(action.passwordUpdate)),
                map(() => profileActions.updatePasswordSuccess()),
                catchError(async () => profileActions.updatePasswordFailure())
            );
        }
    );

    constructor(private userCredentialsService: UserCredentialsService, private actions$: Actions) { }
}
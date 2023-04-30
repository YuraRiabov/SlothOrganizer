import * as profileActions from '@store/actions/profile.actions';

import { Actions, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, repeat, take } from 'rxjs';

import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { UserCredentialsService } from '@api/user-credentials.service';
import { selectUserEmail } from '@store/selectors/auth.selectors';

@Injectable()
export class UserCredentialsEffects {
    public updatePassword$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(profileActions.updatePassword),
                concatLatestFrom(() => this.store.select(selectUserEmail)),
                mergeMap(([action, email]) => this.userCredentialsService.updatePassword({ ...action.passwordUpdate, email }).pipe(take(1))),
                map(() => profileActions.updatePasswordSuccess()),
                catchError(async () => profileActions.updatePasswordFailure()),
                repeat()
            );
        }
    );

    constructor(private userCredentialsService: UserCredentialsService, private actions$: Actions, private store: Store) { }
}
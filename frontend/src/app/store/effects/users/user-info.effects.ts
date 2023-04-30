import * as profileActions from '@store/actions/profile.actions';

import { Actions, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap, take } from 'rxjs';

import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { UserInfoService } from '@api/user-info.service';
import { selectUserId } from '@store/selectors/auth.selectors';

@Injectable()
export class UserInfoEffects {
    public uploadAvatar$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(profileActions.uploadAvatar),
                mergeMap((action) => this.userInfoService.updateAvater(action.image).pipe(take(1))),
                map((user) => profileActions.uploadAvatarSuccess({ url: user.avatarUrl! }))
            );
        }
    );

    public updateFirstName$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(profileActions.updateFirstName),
                concatLatestFrom(() => this.store.select(selectUserId)),
                mergeMap(([action, id]) => this.userInfoService.update({ id, firstName: action.firstName }).pipe(take(1)))
            );
        },
        {
            dispatch: false
        }
    );

    public updateLastName$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(profileActions.updateLastName),
                concatLatestFrom(() => this.store.select(selectUserId)),
                mergeMap(([action, id]) => this.userInfoService.update({ id, lastName: action.lastName }).pipe(take(1)))
            );
        },
        {
            dispatch: false
        }
    );

    public deleteAvatar$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(profileActions.deleteAvatar),
                mergeMap(() => this.userInfoService.deleteAvatar().pipe(take(1)))
            );
        },
        {
            dispatch: false
        }
    );

    constructor(private userInfoService: UserInfoService, private actions$: Actions, private store: Store) { }
}
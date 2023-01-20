import * as profileActions from '@store/actions/profile.actions';

import { Actions, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';

import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { UserInfoService } from '@api/user-info.service';
import { mergeMap } from 'rxjs';
import { selectUserId } from '@store/selectors/auth.selectors';

@Injectable()
export class UserInfoEffects {
    public updateFirstName$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(profileActions.updateFirstName),
                concatLatestFrom(() => this.store.select(selectUserId)),
                mergeMap(([action, id]) => this.userInfoService.update({ id, firstName: action.firstName }))
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
                mergeMap(([action, id]) => this.userInfoService.update({ id, lastName: action.lastName }))
            );
        },
        {
            dispatch: false
        }
    );

    public saveAvatar$ = createEffect(
        () => {
            return this.actions$.pipe(
                ofType(profileActions.saveAvatar),
                concatLatestFrom(() => this.store.select(selectUserId)),
                mergeMap(([action, id]) => this.userInfoService.update({ id, avatarUrl: action.url }))
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
                concatLatestFrom(() => this.store.select(selectUserId)),
                mergeMap(([, id]) => this.userInfoService.deleteAvatar(id))
            );
        },
        {
            dispatch: false
        }
    );

    constructor(private userInfoService: UserInfoService, private actions$: Actions, private store: Store) { }
}
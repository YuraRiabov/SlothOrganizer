// hydration.effects.ts

import * as HydrationActions from '../../actions/hydration.actions';

import { Action, Store } from '@ngrx/store';
import { Actions, OnInitEffects, createEffect, ofType } from '@ngrx/effects';
import { distinctUntilChanged, map, switchMap, tap } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { getEmptyState } from '@utils/creation-functions/auth-state.helper';
import { signInRoute } from '@shared/routes/routes';

@Injectable()
export class HydrationEffects implements OnInitEffects {
    logout$ = createEffect(
        () => {
            return this.action$.pipe(
                ofType(HydrationActions.logoutAction),
                tap(() => {
                    localStorage.setItem('state', JSON.stringify(getEmptyState()));
                    this.router.navigate([signInRoute]);
                })
            );
        },
        { dispatch: false }
    );

    serialize$ = createEffect(
        () => {
            return this.action$.pipe(
                ofType(HydrationActions.hydrateSuccess, HydrationActions.hydrateFailure),
                switchMap(() => this.store),
                distinctUntilChanged(),
                tap((state) => {
                    localStorage.setItem('state', JSON.stringify(state));
                })
            );
        },
        { dispatch: false }
    );

    hydrate$ = createEffect(() => {
        return this.action$.pipe(
            ofType(HydrationActions.hydrate),
            map(() => {
                const storageValue = localStorage.getItem('state');
                if (storageValue) {
                    try {
                        const state = JSON.parse(storageValue);
                        return HydrationActions.hydrateSuccess({ state });
                    } catch {
                        localStorage.removeItem('state');
                    }
                }
                return HydrationActions.hydrateFailure();
            })
        );
    }
    );

    constructor(private action$: Actions, private store: Store, private router: Router) { }

    ngrxOnInitEffects(): Action {
        return HydrationActions.hydrate();
    }
}
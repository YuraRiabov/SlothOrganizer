import { CanActivate, Router, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';

import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectAccessToken } from '@store/selectors/auth-page.selectors';

@Injectable({
    providedIn: 'root'
})
export class AuthorizedGuard implements CanActivate {
    constructor(private store: Store, private router: Router) { }
    canActivate(): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        return this.store.select(selectAccessToken).pipe(map((token) => token ? true : this.router.parseUrl('/auth/sign-in')));
    }
}

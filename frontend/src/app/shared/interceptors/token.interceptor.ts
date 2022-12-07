import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, switchMap } from 'rxjs';

import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectAccessToken } from '@store/selectors/auth-page.selectors';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    constructor(private store: Store) {}
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return this.store.select(selectAccessToken).pipe(
            switchMap((token) => {
                req = req.clone({
                    setHeaders: {
                        Authorization: `Bearer ${token}`
                    }
                });
                return next.handle(req);
            })
        );
    }

}
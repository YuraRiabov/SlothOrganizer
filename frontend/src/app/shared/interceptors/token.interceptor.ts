import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, switchMap } from 'rxjs';

import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectAccessToken } from '@store/selectors/auth.selectors';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    constructor(private store: Store) {}
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (req.url.includes('gyazo')) {
            return next.handle(req);
        }
        return this.store.select(selectAccessToken).pipe(
            switchMap((token) => {
                if (token) {
                    req = req.clone({
                        setHeaders: {
                            Authorization: `Bearer ${token}`
                        }
                    });
                }
                return next.handle(req);
            })
        );
    }

}
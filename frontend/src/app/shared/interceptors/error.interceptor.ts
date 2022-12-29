import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, Subject, catchError, switchMap, tap, throwError } from 'rxjs';

import { AuthService } from '@api/auth.service';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Token } from '#types/auth/token';
import { addToken } from '@store/actions/login-page.actions';
import { logoutAction } from '@store/actions/hydration.actions';
import { selectToken } from '@store/selectors/auth-page.selectors';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    private refreshTokenInProgress = false;

    private tokenRefreshedSource = new Subject<void>();
    private tokenRefreshed$ = this.tokenRefreshedSource.asObservable();

    constructor(private authService: AuthService, private store: Store, private router: Router) { }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError((response) => {
                if (response.status === 401) {
                    return this.refreshToken().pipe(
                        switchMap((token) => {
                            if (token) {
                                req = req.clone({
                                    setHeaders: {
                                        Authorization: `Bearer ${token.accessToken}`
                                    }
                                });
                            }
                            return next.handle(req);
                        })
                    );
                }

                return next.handle(req);
            })
        );
    }

    private refreshToken(): Observable<Token | null> {
        if (this.refreshTokenInProgress) {
            return new Observable(observer => {
                this.tokenRefreshed$.subscribe(() => {
                    observer.next();
                    observer.complete();
                });
            }).pipe(
                switchMap(() => this.store.select(selectToken))
            );
        }
        return this.store.select(selectToken).pipe(
            switchMap((token) => {
                this.refreshTokenInProgress = true;
                return this.authService.refreshToken(token);
            }),
            tap((token) => {
                this.store.dispatch(addToken({ token }));
                this.refreshTokenInProgress = false;
                this.tokenRefreshedSource.next();
            }),
            catchError((error) => {
                this.refreshTokenInProgress = false;
                this.store.dispatch(logoutAction());
                return throwError(() => error);
            }));
    }

}
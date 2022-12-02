import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, catchError, switchMap } from 'rxjs';

import { AuthService } from 'src/app/api/auth.service';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { addToken } from 'src/app/store/actions/login-page.actions';
import { selectToken } from 'src/app/store/selectors/auth-page.selectors';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService, private store: Store, private router: Router) {}
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError((response) => {
                if (response.status === 401) {
                    return this.store.select(selectToken).pipe(
                        switchMap((token) => {
                            return this.authService.refreshToken(token);
                        }),
                        switchMap((token) => {
                            this.store.dispatch(addToken({token}));
                            req = req.clone({
                                setHeaders: {
                                    Authorization: `Bearer ${token.accessToken}`
                                }
                            });

                            return next.handle(req);
                        })
                    );
                }

                if (response.error === 'Invalid refresh token') {
                    this.router.navigate(['auth/sign-in']);
                }

                return next.handle(req);
            })
        );
    }

}
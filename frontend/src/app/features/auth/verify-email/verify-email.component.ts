import { ActivatedRoute, Router } from '@angular/router';
import * as loginPageActions from '@store/actions/login-page.actions';

import { FormControl, Validators } from '@angular/forms';
import { Observable, catchError, concatMap, filter, map, of } from 'rxjs';

import { AuthService } from '@api/auth.service';
import { AuthState } from '@store/states/auth-state';
import { BaseComponent } from '@shared/components/base/base.component';
import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Token } from '#types/auth/token';
import { login } from '@store/actions/login-page.actions';
import { selectUserEmail } from '@store/selectors/auth-page.selectors';

@Component({
    selector: 'app-verify-email',
    templateUrl: './verify-email.component.html',
    styleUrls: ['./verify-email.component.sass']
})
export class VerifyEmailComponent extends BaseComponent {
    private email$: Observable<string>;

    public codeControl: FormControl = new FormControl('', [
        Validators.required,
        Validators.pattern('[0-9]*')
    ]);

    constructor(private authService: AuthService,
        private store: Store,
        private router: Router) {
        super();
        this.email$ = store.select(selectUserEmail);
    }

    public redirectTo(route: string): void {
        this.router.navigate([route]);
    }

    public submit() {
        this.email$.pipe(
            this.untilDestroyed,
            concatMap((email) =>
                this.authService.verifyEmail({
                    email,
                    verificationCode: this.codeControl.value
                })
            ),
            catchError(() => {
                this.codeControl.setErrors({ invalidCode: true });
                return of(null);
            }),
            filter(auth => auth != null),
            map(auth => auth as AuthState)
        ).subscribe((auth) => {
            this.store.dispatch(login({ authState: auth }));
            this.router.navigate(['']);
        });
    }

    public resendCode(): void {
        this.email$
            .pipe(
                this.untilDestroyed,
                concatMap((email) => this.authService.resendCode(email))
            ).subscribe();
    }
}

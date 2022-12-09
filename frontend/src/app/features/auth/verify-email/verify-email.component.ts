import { FormControl, Validators } from '@angular/forms';
import { Observable, catchError, concatMap, map, of } from 'rxjs';
import { addToken, login } from '@store/actions/login-page.actions';

import { AuthService } from '@api/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
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

    constructor(private authService: AuthService, private store: Store, private router: Router) {
        super();
        this.email$ = store.select(selectUserEmail);
    }

    public redirectTo(route: string) : void {
        this.router.navigate([route]);
    }

    public submit() {
        this.email$.pipe(
            this.untilThis,
            concatMap((email) =>
                this.authService.verifyEmail({
                    email,
                    verificationCode: this.codeControl.value
                })
            ),
            map((auth) => {
                this.store.dispatch(login({authState: auth}));
                this.redirectTo('');
                return of(null);
            }),
            catchError(() => {
                this.codeControl.setErrors({ invalidCode: true });
                return of(null);
            })
        ).subscribe();
    }

    public resendCode() {
        this.email$
            .pipe(
                this.untilThis,
                concatMap((email) => this.authService.resendCode(email))
            ).subscribe();
    }
}

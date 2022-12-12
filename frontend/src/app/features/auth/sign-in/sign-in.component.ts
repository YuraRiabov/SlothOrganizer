import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { addUser, login } from '@store/actions/login-page.actions';
import { catchError, filter, map, of } from 'rxjs';
import { getEmailValidators, getPasswordValidators } from '@utils/validators/user-validators.helper';

import { AuthService } from '@api/auth.service';
import { AuthState } from '@store/states/auth-state';
import { BaseComponent } from '@shared/components/base/base.component';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';

@Component({
    selector: 'app-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.sass']
})
export class SignInComponent extends BaseComponent implements OnInit {
    signInGroup: FormGroup = {} as FormGroup;

    constructor(private authService: AuthService,
        private store: Store,
        private router: Router) {
        super();
    }

    ngOnInit(): void {
        this.signInGroup = this.buildSignInForm();
    }

    public redirectTo(route: string): void {
        this.router.navigate([route]);
    }

    public signInClick(): void {
        this.authService.signIn({
            email: this.signInGroup.get('email')?.value,
            password: this.signInGroup.get('password')?.value
        }).pipe(
            this.untilDestroyed,
            catchError(() => {
                this.signInGroup.get('email')?.setErrors({ invalidLogin: true });
                this.signInGroup.get('password')?.setErrors({ invalidLogin: true });
                return of(null);
            }),
            filter(auth => auth != null),
            map(auth => auth as AuthState)
        ).subscribe((auth) => {
            if (auth.token == null) {
                this.store.dispatch(addUser({ user: auth.user }));
                this.redirectTo('auth/verify-email');
                return;
            }
            this.store.dispatch(login({ authState: auth }));
            this.redirectTo('');
        });
    }

    public updateLoginErrors() : void {
        this.signInGroup.get('email')?.setErrors({ invalidLogin: false });
        this.signInGroup.get('password')?.setErrors({ invalidLogin: false });
        this.signInGroup.get('email')?.updateValueAndValidity();
        this.signInGroup.get('password')?.updateValueAndValidity();
    }

    private buildSignInForm(): FormGroup {
        const emailControl = new FormControl('', getEmailValidators());

        const passwordControl = new FormControl('', getPasswordValidators());

        return new FormGroup({
            email: emailControl,
            password: passwordControl
        });
    }
}

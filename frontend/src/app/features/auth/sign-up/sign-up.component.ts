import * as loginPageActions from '@store/actions/login-page.actions';

import { FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError, filter, map, of } from 'rxjs';
import { getEmailValidators, getNameValidators, getPasswordValidators, hasLengthErrors, passwordMatchingValidator } from '@utils/validators/user-validators.helper';

import { AuthService } from '@api/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { User } from '#types/user/user';

@Component({
    selector: 'app-sign-up',
    templateUrl: './sign-up.component.html',
    styleUrls: ['./sign-up.component.sass']
})
export class SignUpComponent extends BaseComponent {

    public signUpGroup: FormGroup;

    constructor(
        private authService: AuthService,
        private store: Store,
        private router: Router
    ) {
        super();
        this.signUpGroup = this.buildSignUpGroup();
    }

    public redirectTo(route: string) : void {
        this.router.navigate([route]);
    }

    public hasLengthErrors(controlName: string): boolean {
        return hasLengthErrors(this.signUpGroup, controlName);
    }

    public signUpClick() {
        this.authService.signUp({
            firstName: this.signUpGroup.get('firstName')?.value!,
            lastName: this.signUpGroup.get('lastName')?.value!,
            email: this.signUpGroup.get('email')?.value!,
            password: this.signUpGroup.get('password')?.value!
        }).pipe(
            this.untilDestroyed,
            catchError(() => {
                this.signUpGroup.get('email')?.setErrors({ emailTaken: true });
                return of(null);
            }),
            filter(user => user != null),
            map(user => user as User)
        ).subscribe((user) => {
            this.store.dispatch(loginPageActions.addUser({ user }));
            this.redirectTo('auth/verify-email');
        });
    }

    private buildSignUpGroup() : FormGroup {
        const firstNameControl = new FormControl('', getNameValidators());

        const lastNameControl = new FormControl('', getNameValidators());

        const emailControl = new FormControl('', getEmailValidators());

        const passwordControl = new FormControl('', getPasswordValidators());

        const repeatPasswordControl = new FormControl('', Validators.required);

        return new FormGroup({
            firstName: firstNameControl,
            lastName: lastNameControl,
            email: emailControl,
            password: passwordControl,
            repeatPassword: repeatPasswordControl
        }, {
            validators: passwordMatchingValidator()
        });
    }
}

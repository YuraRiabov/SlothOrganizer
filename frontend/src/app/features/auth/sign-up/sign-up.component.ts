import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError, map, of } from 'rxjs';
import { getEmailValidators, getNameValidators, getPasswordValidators, passwordMatchingValidator } from 'src/app/utils/validators/user-validators.helper';

import { AuthService } from 'src/app/api/auth.service';
import { BaseComponent } from 'src/app/shared/components/base/base.component';
import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { register } from 'src/app/store/actions/login-page.actions';

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
        private route: ActivatedRoute,
        private router: Router
    ) {
        super();
        this.signUpGroup = this.buildSignUpGroup();
    }

    public signUpClick() {
        this.authService.signUp({
            firstName: this.signUpGroup.get('firstName')?.value!,
            lastName: this.signUpGroup.get('lastName')?.value!,
            email: this.signUpGroup.get('email')?.value!,
            password: this.signUpGroup.get('password')?.value!
        }).pipe(
            this.untilThis,
            map((user) => {
                if (user == null) {
                    return;
                }
                this.store.dispatch(register({ user }));
                this.router.navigate(['verify-email'], {
                    relativeTo: this.route.parent
                });
                return of(null);
            }),
            catchError((resp) => {
                if (resp.error === 'Account with this email already exists') {
                    this.signUpGroup.get('email')?.setErrors({ emailTaken: true });
                }
                return of(null);
            })
        ).subscribe();
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

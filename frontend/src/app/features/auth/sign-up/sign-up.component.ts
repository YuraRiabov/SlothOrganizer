import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, of } from 'rxjs';

import { AuthService } from 'src/app/api/auth.service';
import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { register } from 'src/app/store/actions/login-page.actions';

@Component({
    selector: 'app-sign-up',
    templateUrl: './sign-up.component.html',
    styleUrls: ['./sign-up.component.sass']
})
export class SignUpComponent {


    public signUpGroup: FormGroup;

    constructor(
        private authService: AuthService,
        private store: Store,
        private route: ActivatedRoute,
        private router: Router
    ) {
        this.signUpGroup = this.buildSignUpGroup();
    }

    public signUpClick() {
        this.authService.signUp({
            firstName: this.signUpGroup.get('firstName')?.value!,
            lastName: this.signUpGroup.get('lastName')?.value!,
            email: this.signUpGroup.get('email')?.value!,
            password: this.signUpGroup.get('password')?.value!
        }).pipe(
            catchError((resp) => {
                if (resp.error === 'Account with this email already exists') {
                    this.signUpGroup.get('email')?.setErrors({ emailTaken: true });
                }
                return of(null);
            })
        ).subscribe(
            (user) => {
                if (user == null) {
                    return;
                }
                this.store.dispatch(register({ user }));
                this.router.navigate(['verify-email'], {
                    relativeTo: this.route.parent
                });
            }
        );
    }

    private buildSignUpGroup() : FormGroup {
        const firstNameControl = new FormControl('', [
            Validators.required,
            Validators.minLength(2),
            Validators.maxLength(30)
        ]);

        const lastNameControl = new FormControl('', [
            Validators.required,
            Validators.minLength(2),
            Validators.maxLength(30)
        ]);

        const emailControl = new FormControl('', [
            Validators.required,
            Validators.email
        ]);

        const passwordControl = new FormControl('', [
            Validators.required,
            Validators.minLength(8),
            Validators.maxLength(16),
            Validators.pattern('([0-9].*[a-zA-Z])|([a-zA-Z].*[0-9])')
        ]);

        const repeatPasswordControl = new FormControl('', [Validators.required]);

        return new FormGroup({
            firstName: firstNameControl,
            lastName: lastNameControl,
            email: emailControl,
            password: passwordControl,
            repeatPassword: repeatPasswordControl
        }, {
            validators: this.passwordValidator()
        });
    }

    private passwordValidator() : ValidatorFn {
        return (group: AbstractControl) => {
            const passwordControl = group.get('password');
            const repeatPasswordControl = group.get('repeatPassword');
            if ( passwordControl?.value !== repeatPasswordControl?.value ) {
                repeatPasswordControl?.setErrors({ passwordMismatch: true });
            }
            return null;
        };
    }
}

import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError, of } from 'rxjs';
import { login, register } from 'src/app/store/actions/login-page.actions';

import { AuthService } from 'src/app/api/auth.service';
import { Store } from '@ngrx/store';

@Component({
    selector: 'app-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.sass']
})
export class SignInComponent implements OnInit {
    signInGroup: FormGroup = {} as FormGroup;

    constructor(private authService: AuthService,
        private store: Store,
        private router: Router) {}

    ngOnInit(): void {
        this.signInGroup = this.buildSignInForm();
    }

    public redirectTo(route: string) : void {
        this.router.navigate([route]);
    }

    public signInClick(): void {
        this.authService.signIn({
            email: this.signInGroup.get('email')?.value,
            password: this.signInGroup.get('password')?.value
        }).pipe(
            catchError((resp) => {
                if (resp.error === 'No user with such email' || resp.error === 'Invalid password') {
                    this.signInGroup.get('email')?.setErrors({ invalidLogin: true});
                }
                return of(null);
            })
        ).subscribe((auth) => {
            if (auth == null) {
                return;
            }
            if (auth.token == null) {
                this.store.dispatch(register( { user: auth.user }));
                this.redirectTo('auth/verify-email');
                return;
            }
            this.store.dispatch(login({authState: auth}));
            this.redirectTo('');
        });
    }

    private buildSignInForm(): FormGroup {
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

        return new FormGroup({
            email: emailControl,
            password: passwordControl
        });
    }
}

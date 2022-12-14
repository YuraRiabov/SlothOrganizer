import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable, concatMap } from 'rxjs';
import { getPasswordValidators, passwordMatchingValidator } from '@utils/validators/user-validators.helper';

import { AuthService } from '@api/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { Store } from '@ngrx/store';
import { login } from '@store/actions/login-page.actions';
import { selectUserEmail } from '@store/selectors/auth-page.selectors';

@Component({
    selector: 'app-reset-password',
    templateUrl: './reset-password.component.html',
    styleUrls: ['./reset-password.component.sass']
})
export class ResetPasswordComponent extends BaseComponent implements OnInit {
    private email: string = '';
    private code: string = '';

    public resetPassowordGroup: FormGroup = {} as FormGroup;

    constructor(private authService: AuthService,
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private store: Store) {
        super();
    }

    ngOnInit(): void {
        this.resetPassowordGroup = this.buildResetPasswordGroup();
        this.activatedRoute.queryParams
            .pipe(this.untilDestroyed)
            .subscribe((params) => {
                this.email = params['email'];
                this.code = params['code'];
            });
    }

    public submitClick(): void {
        this.authService.resetPassword({
            password: this.resetPassowordGroup.get('password')?.value,
            email: this.email,
            code: this.code
        }).pipe(
            this.untilDestroyed
        ).subscribe((auth) => {
            this.store.dispatch(login({ authState: auth }));
            this.router.navigate(['']);
        });
    }

    private buildResetPasswordGroup(): FormGroup {
        var passwordControl = new FormControl('', getPasswordValidators());
        var repeatPasswordControl = new FormControl('', Validators.required);
        return new FormGroup({
            password: passwordControl,
            repeatPassword: repeatPasswordControl
        }, {
            validators: passwordMatchingValidator()
        });
    }
}

import * as authActions from '@store/actions/auth.actions';

import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { getPasswordValidators, passwordMatchingValidator } from '@utils/validators/user-validators.helper';

import { AuthService } from '@api/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { Store } from '@ngrx/store';
import { UserCredentialsService } from '@api/user-credentials.service';
import { hasLengthErrors } from '@utils/validators/common-validators';
import { rootRoute } from '@shared/routes/routes';

@Component({
    selector: 'so-reset-password',
    templateUrl: './reset-password.component.html',
    styleUrls: ['./reset-password.component.sass']
})
export class ResetPasswordComponent extends BaseComponent implements OnInit {
    private email: string = '';
    private code: string = '';

    public resetPassowordGroup: FormGroup = {} as FormGroup;

    constructor(
        private userCredentialsService: UserCredentialsService,
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private store: Store
    ) {
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

    public hasLengthErrors(controlName: string): boolean {
        return hasLengthErrors(this.resetPassowordGroup, controlName);
    }

    public resetPassword(): void {
        this.userCredentialsService.resetPassword({
            password: this.resetPassowordGroup.get('password')?.value,
            email: this.email,
            code: this.code
        }).pipe(
            this.untilDestroyed
        ).subscribe((auth) => {
            this.store.dispatch(authActions.login({ authState: auth }));
            this.router.navigate([rootRoute]);
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

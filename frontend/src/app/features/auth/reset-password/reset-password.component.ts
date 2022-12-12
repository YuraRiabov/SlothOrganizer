import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable, concatMap } from 'rxjs';
import { getPasswordValidators, passwordMatchingValidator } from '@utils/validators/user-validators.helper';

import { BaseComponent } from '@shared/components/base/base.component';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { UsersService } from '@api/users.service';
import { selectUserEmail } from '@store/selectors/auth-page.selectors';

@Component({
    selector: 'app-reset-password',
    templateUrl: './reset-password.component.html',
    styleUrls: ['./reset-password.component.sass']
})
export class ResetPasswordComponent extends BaseComponent implements OnInit {
    private userEmail$: Observable<string>;

    public resetPassowordGroup: FormGroup = {} as FormGroup;

    constructor(private userService: UsersService, private router: Router, private store: Store) {
        super();
        this.userEmail$ = store.select(selectUserEmail);
    }

    ngOnInit(): void {
        this.resetPassowordGroup = this.buildResetPasswordGroup();
    }

    public submitClick() : void {
        this.userEmail$.pipe(
            this.untilDestroyed,
            concatMap((email) => {
                return this.userService.resetPassword({
                    password: this.resetPassowordGroup.get('password')?.value,
                    email
                });
            }),
        ).subscribe(() => {
            this.router.navigate(['']);
        });
    }

    private buildResetPasswordGroup() : FormGroup {
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

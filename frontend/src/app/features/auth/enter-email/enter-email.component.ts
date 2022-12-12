import { Component, OnInit } from '@angular/core';
import { catchError, filter, map, of } from 'rxjs';

import { AuthService } from '@api/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { addEmail } from '@store/actions/login-page.actions';
import { getEmailValidators } from '@utils/validators/user-validators.helper';

@Component({
    selector: 'app-enter-email',
    templateUrl: './enter-email.component.html',
    styleUrls: ['./enter-email.component.sass']
})
export class EnterEmailComponent extends BaseComponent implements OnInit {
    public emailControl: FormControl = {} as FormControl;

    constructor(private router: Router, private authService: AuthService, private store: Store) {
        super();
    }

    ngOnInit(): void {
        this.emailControl = new FormControl('', getEmailValidators());
    }

    public submitClick(): void {
        this.authService.resendCode(this.emailControl.value).pipe(
            this.untilDestroyed,
            catchError((resp) => {
                if (resp.status === 404) {
                    this.emailControl.setErrors({ email: true });
                }
                return of(null);
            })
        ).subscribe(() => {
            if (!this.emailControl.valid) {
                return;
            }
            this.store.dispatch(addEmail({email: this.emailControl.value}));
            this.redirectTo('auth/verify-email/true');
        });
    }

    private redirectTo(path: string): void {
        this.router.navigate([ path ]);
    }
}

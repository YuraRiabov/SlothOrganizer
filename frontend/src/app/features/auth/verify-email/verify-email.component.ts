import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable, catchError, concatMap, map, of } from 'rxjs';

import { AuthService } from 'src/app/api/auth.service';
import { BaseComponent } from 'src/app/shared/components/base/base.component';
import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectUserId } from 'src/app/store/selectors/auth-page.selectors';
import { verifyEmail } from 'src/app/store/actions/login-page.actions';

@Component({
    selector: 'app-verify-email',
    templateUrl: './verify-email.component.html',
    styleUrls: ['./verify-email.component.sass']
})
export class VerifyEmailComponent extends BaseComponent {
    private userId$: Observable<number>;

    public codeControl: FormControl = new FormControl('', [
        Validators.required,
        Validators.pattern('[0-9]*')
    ]);

    constructor(private authService: AuthService, private store: Store) {
        super();
        this.userId$ = store.select(selectUserId);
    }

    public submit() {
        this.userId$.pipe(
            this.untilThis,
            concatMap((id) =>
                this.authService.verifyEmail({
                    userId: id,
                    verificationCode: this.codeControl.value
                })
            ),
            map((token) => {
                this.store.dispatch(verifyEmail({ token }));
                return of(null);
            }),
            catchError(() => {
                this.codeControl.setErrors({ invalidCode: true });
                return of(null);
            })
        ).subscribe();
    }

    public resendCode() {
        this.userId$
            .pipe(
                this.untilThis,
                concatMap((id) => this.authService.resendCode(id))
            ).subscribe();
    }
}

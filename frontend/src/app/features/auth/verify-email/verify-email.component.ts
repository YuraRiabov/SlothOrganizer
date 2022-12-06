import { FormControl, Validators } from '@angular/forms';
import { Observable, catchError, concatMap, map, of } from 'rxjs';

import { AuthService } from '@api/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { addToken } from '@store/actions/login-page.actions';
import { selectUserId } from '@store/selectors/auth-page.selectors';

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

    constructor(private authService: AuthService, private store: Store, private router: Router) {
        super();
        this.userId$ = store.select(selectUserId);
    }

    public redirectTo(route: string) : void {
        this.router.navigate([route]);
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
                this.store.dispatch(addToken({ token }));
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

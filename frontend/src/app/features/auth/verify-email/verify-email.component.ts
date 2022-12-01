import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable, catchError, concatMap, of } from 'rxjs';

import { AuthService } from 'src/app/api/auth.service';
import { AuthState } from 'src/app/types/states/authState';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { selectUserId } from 'src/app/store/selectors/auth-page.selectors';
import { verifyEmail } from 'src/app/store/actions/login-page.actions';

@Component({
    selector: 'app-verify-email',
    templateUrl: './verify-email.component.html',
    styleUrls: ['./verify-email.component.sass']
})
export class VerifyEmailComponent {
    private userId$: Observable<number>;

    public codeControl: FormControl = new FormControl('', [
        Validators.required,
        Validators.pattern('[0-9]*')
    ]);

    constructor(private authService: AuthService, private store: Store, private router: Router) {
        this.userId$ = store.select(selectUserId);
    }

    public redirectTo(route: string) : void {
        this.router.navigate([route]);
    }

    public submit() {
        this.userId$.pipe(
            concatMap((id) =>
                this.authService.verifyEmail({
                    userId: id,
                    verificationCode: this.codeControl.value
                })
            ),
            catchError(() => {
                this.codeControl.setErrors({ invalidCode: true });
                return of(null);
            })
        ).subscribe((token) => {
            if (token == null) {
                return;
            }
            this.store.dispatch(verifyEmail({ token }));
            this.redirectTo('');
        });
    }

    public resendCode() {
        this.userId$
            .pipe(concatMap((id) => this.authService.resendCode(id)))
            .subscribe();
    }
}

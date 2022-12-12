import { FormControl, Validators } from '@angular/forms';
import { Observable, catchError, concatMap, filter, map, of } from 'rxjs';

import { AuthService } from '@api/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Token } from '#types/auth/token';
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
            this.untilDestroyed,
            concatMap((id) =>
                this.authService.verifyEmail({
                    userId: id,
                    verificationCode: this.codeControl.value
                })
            ),
            catchError(() => {
                this.codeControl.setErrors({ invalidCode: true });
                return of(null);
            }),
            filter(user => user != null),
            map(token => token as Token)
        ).subscribe((token) => {
            this.store.dispatch(addToken({ token }));
        });
    }

    public resendCode() {
        this.userId$
            .pipe(
                this.untilDestroyed,
                concatMap((id) => this.authService.resendCode(id))
            ).subscribe();
    }
}

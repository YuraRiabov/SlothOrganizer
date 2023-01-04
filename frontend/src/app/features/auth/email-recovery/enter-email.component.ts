import { Component, OnInit } from '@angular/core';
import { catchError, of } from 'rxjs';

import { AuthService } from '@api/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { FormControl } from '@angular/forms';
import { Store } from '@ngrx/store';
import { getEmailValidators } from '@utils/validators/user-validators.helper';

@Component({
    selector: 'so-enter-email',
    templateUrl: './email-recovery.component.html',
    styleUrls: ['./email-recovery.component.sass']
})
export class EmailRecoveryComponent extends BaseComponent implements OnInit {
    public submittedEmail: string = '';

    public emailSent: boolean = false;

    public emailControl: FormControl = {} as FormControl;

    constructor(private authService: AuthService, private store: Store) {
        super();
    }

    ngOnInit(): void {
        this.emailControl = new FormControl('', getEmailValidators());
    }

    public sendPasswordResetLink(): void {
        this.authService.sendPasswordResetLink(this.emailControl.value).pipe(
            this.untilDestroyed,
            catchError(() => {
                this.emailControl.setErrors({ email: true });
                return of(null);
            })
        ).subscribe(() => {
            if(!this.emailControl.valid) {
                return;
            }
            this.submittedEmail = this.emailControl.value;
            this.emailSent = true;
        });
    }
}

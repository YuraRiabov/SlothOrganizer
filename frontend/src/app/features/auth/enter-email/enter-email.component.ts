import { Component, OnInit } from '@angular/core';
import { catchError, of } from 'rxjs';

import { AuthService } from '@api/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { FormControl } from '@angular/forms';
import { Store } from '@ngrx/store';
import { getEmailValidators } from '@utils/validators/user-validators.helper';

@Component({
    selector: 'app-enter-email',
    templateUrl: './enter-email.component.html',
    styleUrls: ['./enter-email.component.sass']
})
export class EnterEmailComponent extends BaseComponent implements OnInit {
    public emailControl: FormControl = {} as FormControl;

    constructor(private authService: AuthService, private store: Store) {
        super();
    }

    ngOnInit(): void {
        this.emailControl = new FormControl('', getEmailValidators());
    }

    public submitClick(): void {
        this.authService.sendPasswordReset(this.emailControl.value).pipe(
            this.untilDestroyed,
            catchError(() => {
                this.emailControl.setErrors({ email: true });
                return of(null);
            })
        ).subscribe();
    }
}

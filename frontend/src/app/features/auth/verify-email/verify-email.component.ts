import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable, concatMap } from 'rxjs';

import { AuthService } from 'src/app/api/auth.service';
import { AuthState } from 'src/app/types/states/authState';
import { Component } from '@angular/core';
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

  constructor(private authService: AuthService, private store: Store) {
    this.userId$ = store.select(selectUserId);
  }

  public submit() {
    this.userId$
      .pipe(
        concatMap((id) =>
          this.authService.verifyEmail({
            userId: id,
            verificationCode: this.codeControl.value
          })
        )
      )
      .subscribe(
        (token) => this.store.dispatch(verifyEmail({ token })),
        () => this.codeControl.setErrors({ invalidCode: true })
      );
  }

  public resendCode() {
    this.userId$
      .pipe(concatMap((id) => this.authService.resendCode(id)))
      .subscribe();
  }
}

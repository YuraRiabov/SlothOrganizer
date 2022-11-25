import { FormControl, FormGroup, Validators } from '@angular/forms';

import { AuthService } from 'src/app/api/auth.service';
import { Component } from '@angular/core';
import { NewUser } from 'src/app/types/user/newUser';
import { Store } from '@ngrx/store';
import { register } from 'src/app/store/actions/login-page.actions';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.sass']
})
export class SignUpComponent {
  public firstNameControl = new FormControl('', [
    Validators.required,
    Validators.minLength(2),
    Validators.maxLength(30)
  ]);

  public lastNameControl = new FormControl('', [
    Validators.required,
    Validators.minLength(2),
    Validators.maxLength(30)
  ]);

  public emailControl = new FormControl('', [
    Validators.required,
    Validators.email
  ]);

  public passwordControl = new FormControl('', [
    Validators.required,
    Validators.minLength(8),
    Validators.maxLength(16),
    Validators.pattern('([0-9].*[a-zA-Z])|([a-zA-Z].*[0-9])')
  ]);

  public repeatPasswordControl = new FormControl('', [Validators.required]);

  public signUpGroup = new FormGroup({
    firstName: this.firstNameControl,
    lastName: this.lastNameControl,
    email: this.emailControl,
    password: this.passwordControl,
    repeatPassword: this.repeatPasswordControl
  });

  constructor(private userService: AuthService, private store: Store) {}

  public signUpClick() {
    if (this.passwordControl.value !== this.repeatPasswordControl.value) {
      this.repeatPasswordControl.setErrors({ repeatPassword: true });
      return;
    }
    this.userService
      .signUp({
        firstName: this.firstNameControl.value!,
        lastName: this.lastNameControl.value!,
        email: this.emailControl.value!,
        password: this.passwordControl.value!
      })
      .subscribe(
        (user) => {
          this.store.dispatch(register({ user }));
        },
        (resp) => {
          if (resp.error === 'Account with this email already exists') {
            this.emailControl.setErrors({ emailTaken: true });
          }
        }
      );
  }
}

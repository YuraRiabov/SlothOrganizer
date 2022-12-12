import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AuthPageComponent } from './auth-page/auth-page.component';
import { AuthRoutingModule } from './auth-routing.module';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { VerifyEmailComponent } from './verify-email/verify-email.component';
import { EnterEmailComponent } from './enter-email/enter-email.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

@NgModule({
    declarations: [AuthPageComponent, SignInComponent, SignUpComponent, VerifyEmailComponent, EnterEmailComponent, ResetPasswordComponent],
    imports: [
        CommonModule,
        AuthRoutingModule,
        MaterialModule,
        FormsModule,
        ReactiveFormsModule
    ]
})
export class AuthModule {}

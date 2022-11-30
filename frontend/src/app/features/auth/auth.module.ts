import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AuthPageComponent } from './auth-page/auth-page.component';
import { AuthRoutingModule } from '../../routes/auth-routing.module';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/app/shared/material/material.module';
import { NgModule } from '@angular/core';
import { SignUpComponent } from './sign-up/sign-up.component';
import { VerifyEmailComponent } from './verify-email/verify-email.component';

@NgModule({
    declarations: [AuthPageComponent, SignUpComponent, VerifyEmailComponent],
    imports: [
        CommonModule,
        AuthRoutingModule,
        MaterialModule,
        FormsModule,
        ReactiveFormsModule
    ],
    exports: [ReactiveFormsModule]
})
export class AuthModule {}

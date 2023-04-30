import { RouterModule, Routes } from '@angular/router';

import { AuthPageComponent } from './auth-page/auth-page.component';
import { EmailRecoveryComponent } from './email-recovery/enter-email.component';
import { NgModule } from '@angular/core';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { VerifyEmailComponent } from './verify-email/verify-email.component';

const routes: Routes = [
    {
        path: '',
        component: AuthPageComponent,
        children: [
            {
                path: 'sign-up',
                component: SignUpComponent
            },
            {
                path: 'verify-email',
                component: VerifyEmailComponent
            },
            {
                path: 'sign-in',
                component: SignInComponent
            },
            {
                path: 'enter-email',
                component: EmailRecoveryComponent
            },
            {
                path: 'reset-password',
                component: ResetPasswordComponent
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AuthRoutingModule {}

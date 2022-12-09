import { RouterModule, Routes } from '@angular/router';

import { AuthPageComponent } from '../features/auth/auth-page/auth-page.component';
import { EnterEmailComponent } from '../features/auth/enter-email/enter-email.component';
import { NgModule } from '@angular/core';
import { ResetPasswordComponent } from '../features/auth/reset-password/reset-password.component';
import { SignInComponent } from '../features/auth/sign-in/sign-in.component';
import { SignUpComponent } from '../features/auth/sign-up/sign-up.component';
import { VerifyEmailComponent } from '../features/auth/verify-email/verify-email.component';

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
                path: 'verify-email/:resetPassword',
                component: VerifyEmailComponent
            },
            {
                path: 'sign-in',
                component: SignInComponent
            },
            {
                path: 'enter-email',
                component: EnterEmailComponent
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

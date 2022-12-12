import { RouterModule, Routes } from '@angular/router';

import { AuthPageComponent } from './auth-page/auth-page.component';
import { NgModule } from '@angular/core';
import { SignInComponent } from '../features/auth/sign-in/sign-in.component';
import { SignUpComponent } from '../features/auth/sign-up/sign-up.component';
import { VerifyEmailComponent } from '../features/auth/verify-email/verify-email.component';
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
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AuthRoutingModule {}

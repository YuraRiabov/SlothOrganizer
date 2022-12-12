import { RouterModule, Routes } from '@angular/router';

import { AuthPageComponent } from './auth-page/auth-page.component';
import { NgModule } from '@angular/core';
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
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AuthRoutingModule {}

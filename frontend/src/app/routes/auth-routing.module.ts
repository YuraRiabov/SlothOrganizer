import { RouterModule, Routes } from '@angular/router';

import { AuthPageComponent } from '../features/auth/auth-page/auth-page.component';
import { NgModule } from '@angular/core';
import { SignUpComponent } from '../features/auth/sign-up/sign-up.component';

const routes: Routes = [
  {
    path: '',
    component: AuthPageComponent,
    children: [
      {
        path: 'sign-up',
        component: SignUpComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule {}

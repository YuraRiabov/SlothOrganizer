import { AuthPageComponent } from './auth-page/auth-page.component';
import { AuthRoutingModule } from '../../routes/auth-routing.module';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/app/shared/material/material.module';
import { NgModule } from '@angular/core';
import { SignUpComponent } from './sign-up/sign-up.component';

@NgModule({
  declarations: [AuthPageComponent, SignUpComponent],
  imports: [CommonModule, AuthRoutingModule, MaterialModule]
})
export class AuthModule {}

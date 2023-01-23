import { CommonModule } from '@angular/common';
import { EffectsModule } from '@ngrx/effects';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { PasswordUpdateComponent } from './password-update/password-update.component';
import { PersonalInfoComponent } from './personal-info/personal-info.component';
import { ProfileComponent } from './profile/profile.component';
import { ProfileRoutingModule } from './profile-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { UserCredentialsEffects } from '@store/effects/users/user-credentials.effects';
import { UserInfoEffects } from '@store/effects/users/user-info.effects';

@NgModule({
    declarations: [
        ProfileComponent,
        PersonalInfoComponent,
        PasswordUpdateComponent
    ],
    imports: [
        CommonModule,
        ProfileRoutingModule,
        MaterialModule,
        ReactiveFormsModule,
        EffectsModule.forFeature([UserCredentialsEffects, UserInfoEffects])
    ]
})
export class ProfileModule { }

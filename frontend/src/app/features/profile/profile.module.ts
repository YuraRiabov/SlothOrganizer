import { CommonModule } from '@angular/common';
import { EffectsModule } from '@ngrx/effects';
import { GyazoEffects } from '@store/effects/users/gyazo.effects';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { PersonalInfoComponent } from './personal-info/personal-info.component';
import { ProfileComponent } from './profile/profile.component';
import { ProfileRoutingModule } from './profile-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { UserCredentialsEffects } from '@store/effects/users/user-credentials.effects';
import { UserInfoEffects } from '@store/effects/users/user-info.effects';

@NgModule({
    declarations: [
        ProfileComponent,
        PersonalInfoComponent
    ],
    imports: [
        CommonModule,
        ProfileRoutingModule,
        MaterialModule,
        ReactiveFormsModule,
        EffectsModule.forFeature([UserCredentialsEffects, UserInfoEffects, GyazoEffects])
    ]
})
export class ProfileModule { }

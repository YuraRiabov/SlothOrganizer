import { CommonModule } from '@angular/common';
import { EffectsModule } from '@ngrx/effects';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { PasswordUpdateComponent } from './password-update/password-update.component';
import { PersonalInfoComponent } from './personal-info/personal-info.component';
import { ProfileComponent } from './profile/profile.component';
import { ProfileRoutingModule } from './profile-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@shared/components/shared/shared.module';
import { UserCredentialsEffects } from '@store/effects/users/user-credentials.effects';
import { UserInfoEffects } from '@store/effects/users/user-info.effects';
import { CalendarTabComponent } from './calendar-tab/calendar-tab.component';
import { CalendarEffects } from '@store/effects/calendar/calendar.effects';

@NgModule({
    declarations: [
        ProfileComponent,
        PersonalInfoComponent,
        PasswordUpdateComponent,
        CalendarTabComponent
    ],
    imports: [
        CommonModule,
        ProfileRoutingModule,
        MaterialModule,
        ReactiveFormsModule,
        SharedModule,
        EffectsModule.forFeature([UserCredentialsEffects, UserInfoEffects, CalendarEffects])
    ]
})
export class ProfileModule { }

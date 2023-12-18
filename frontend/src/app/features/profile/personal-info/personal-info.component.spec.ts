import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '@shared/material/material.module';
import { PersonalInfoComponent } from './personal-info.component';
import { ProfileRoutingModule } from '../profile-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { User } from '#types/user/user';
import { updateFirstName } from '@store/actions/profile.actions';

describe('PersonalInfoComponent', () => {
    let component: PersonalInfoComponent;
    let fixture: ComponentFixture<PersonalInfoComponent>;

    const user: User = {
        id: 1,
        firstName: 'first',
        lastName: 'last',
        email: 'email',
        calendar: null
    };

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [PersonalInfoComponent],
            imports: [
                BrowserAnimationsModule,
                CommonModule,
                ProfileRoutingModule,
                MaterialModule,
                ReactiveFormsModule
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(PersonalInfoComponent);
        component = fixture.componentInstance;
        component.user = user;
        fixture.detectChanges();
        spyOn(component.avatarUpdated, 'emit');
        spyOn(component.firstNameUpdated, 'emit');
        spyOn(component.lastNameUpdated, 'emit');
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should update first name on valid change', () => {
        component.firstNameControl.setValue('valid');
        fixture.detectChanges();
        component.updateFirstName();

        expect(component.firstNameUpdated.emit).toHaveBeenCalledTimes(1);
    });

    it('should update last name on valid change', () => {
        component.lastNameControl.setValue('valid');
        fixture.detectChanges();
        component.updateLastName();

        expect(component.lastNameUpdated.emit).toHaveBeenCalledTimes(1);
    });

    it('should delete avatar on delete', () => {
        component.deleteAvatar();

        expect(component.avatarUpdated.emit).toHaveBeenCalledOnceWith(null);
    });
});

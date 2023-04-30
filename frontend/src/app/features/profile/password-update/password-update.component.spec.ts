import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '@shared/material/material.module';
import { PasswordUpdateComponent } from './password-update.component';
import { ProfileRoutingModule } from '../profile-routing.module';
import { ReactiveFormsModule } from '@angular/forms';

describe('PasswordUpdateComponent', () => {
    let component: PasswordUpdateComponent;
    let fixture: ComponentFixture<PasswordUpdateComponent>;
    let button: HTMLElement;

    const setUpValidForm = () => {
        component.passwordUpdateGroup.get('oldPassword')?.setValue('old');
        component.passwordUpdateGroup.get('password')?.setValue('newPassw0rd');
        component.passwordUpdateGroup.get('repeatPassword')?.setValue('newPassw0rd');
    };

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [PasswordUpdateComponent],
            imports: [
                BrowserAnimationsModule,
                CommonModule,
                ProfileRoutingModule,
                MaterialModule,
                ReactiveFormsModule
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(PasswordUpdateComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
        button = fixture.debugElement.query(By.css('button')).nativeElement;
        spyOn(component.updatePassword, 'emit');
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should reset when form valid', () => {
        setUpValidForm();
        fixture.detectChanges();

        button.click();

        expect(component.updatePassword.emit).toHaveBeenCalledTimes(1);
    });

    it('should be invalid when password mismatch', () => {
        setUpValidForm();
        component.passwordUpdateGroup.get('password')?.setValue('newPass0rd');
        fixture.detectChanges();

        expect(component.passwordUpdateGroup.valid).toBeFalse();
    });

    it('should be invalid when password invalid', () => {
        setUpValidForm();
        component.passwordUpdateGroup.get('password')?.setValue('newPassrd');
        component.passwordUpdateGroup.get('repeatPassword')?.setValue('newPassrd');
        fixture.detectChanges();

        expect(component.passwordUpdateGroup.valid).toBeFalse();
    });

    it('should be invalid when old password empty', () => {
        setUpValidForm();
        component.passwordUpdateGroup.get('oldPassword')?.setValue('');
        fixture.detectChanges();

        expect(component.passwordUpdateGroup.valid).toBeFalse();
    });

    it('should be invalid when incorrect old password', () => {
        setUpValidForm();
        component.incorrectPassword = true;
        fixture.detectChanges();

        expect(component.passwordUpdateGroup.valid).toBeFalse();
    });
});

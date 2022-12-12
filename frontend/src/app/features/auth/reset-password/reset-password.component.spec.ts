import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AuthRoutingModule } from '../auth-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '@shared/material/material.module';
import { ResetPasswordComponent } from './reset-password.component';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { UsersService } from '@api/users.service';
import { of } from 'rxjs';

describe('ResetPasswordComponent', () => {
    let component: ResetPasswordComponent;
    let fixture: ComponentFixture<ResetPasswordComponent>;
    let usersService: jasmine.SpyObj<UsersService>;
    let store: jasmine.SpyObj<Store>;
    let router: jasmine.SpyObj<Router>;
    let button: HTMLElement;

    const setUpValidForm = () => {
        component.resetPassowordGroup.get('password')?.setValue('testtest8');
        component.resetPassowordGroup.get('repeatPassword')?.setValue('testtest8');
    };
    beforeEach(async () => {
        usersService = jasmine.createSpyObj(['resetPassword']);
        store = jasmine.createSpyObj(['select']);
        router = jasmine.createSpyObj(['navigate']);
        usersService.resetPassword.and.returnValue(of(null));
        store.select.and.returnValue(of('test@test.com'));
        await TestBed.configureTestingModule({
            declarations: [ResetPasswordComponent],
            imports: [
                CommonModule,
                AuthRoutingModule,
                MaterialModule,
                FormsModule,
                ReactiveFormsModule,
                BrowserAnimationsModule
            ],
            providers: [
                { provide: UsersService, useValue: usersService },
                { provide: Store, useValue: store },
                { provide: Router, useValue: router }
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(ResetPasswordComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();

        button = fixture.debugElement.query(By.css('button')).nativeElement;
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should be valid when valid data', () => {
        setUpValidForm();
        fixture.detectChanges();

        expect(component.resetPassowordGroup.valid).toBeTrue();
    });

    it('should redirect when valid data', () => {
        setUpValidForm();
        fixture.detectChanges();

        button.click();

        expect(usersService.resetPassword).toHaveBeenCalledTimes(1);
        expect(router.navigate).toHaveBeenCalledOnceWith(['']);
    });

    it('should be disabled if invalid password length', () => {
        setUpValidForm();
        component.resetPassowordGroup.controls['password'].setValue('testtest88aaaaaaa');
        fixture.detectChanges();

        expect(component.resetPassowordGroup.get('password')?.hasError('maxlength')).toBeTrue();
    });

    it('should be disabled if invalid password pattern', () => {
        setUpValidForm();
        component.resetPassowordGroup.controls['password'].setValue('testtestaaaaaa');
        fixture.detectChanges();

        expect(component.resetPassowordGroup.get('password')?.hasError('pattern')).toBeTrue();
    });

    it('should be disabled if passwords dont match', () => {
        setUpValidForm();
        component.resetPassowordGroup.controls['repeatPassword'].setValue('testtest88');
        fixture.detectChanges();

        button.click();
        fixture.detectChanges();

        expect(usersService.resetPassword).toHaveBeenCalledTimes(0);
        expect(component.resetPassowordGroup.get('repeatPassword')?.hasError('passwordMismatch')).toBeTrue();
    });
});

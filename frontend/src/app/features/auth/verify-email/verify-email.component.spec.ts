/* eslint-disable no-undef */

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { addToken, login } from '@store/actions/login-page.actions';
import { of, throwError } from 'rxjs';

import { AuthRoutingModule } from '@routes/auth-routing.module';
import { AuthService } from '@api/auth.service';
import { AuthState } from '@store/states/auth-state';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '@shared/material/material.module';
import { Store } from '@ngrx/store';
import { Token } from '#types/auth/token';
import { VerifyEmailComponent } from './verify-email.component';

describe('VerifyEmailComponent', () => {
    let component: VerifyEmailComponent;
    let fixture: ComponentFixture<VerifyEmailComponent>;
    let mockAuthService: jasmine.SpyObj<AuthService>;
    let mockStore: jasmine.SpyObj<Store>;
    let button: HTMLElement;

    beforeEach(async () => {
        mockAuthService = jasmine.createSpyObj(['verifyEmail', 'resendCode']);
        mockStore = jasmine.createSpyObj(['select', 'dispatch']);
        mockStore.select.and.returnValue(of(1));
        await TestBed.configureTestingModule({
            imports: [
                CommonModule,
                AuthRoutingModule,
                MaterialModule,
                FormsModule,
                ReactiveFormsModule,
                BrowserAnimationsModule
            ],
            declarations: [VerifyEmailComponent],
            providers: [
                { provide: AuthService, useValue: mockAuthService },
                { provide: Store, useValue: mockStore },
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(VerifyEmailComponent);
        component = fixture.componentInstance;
        button = fixture.debugElement.query(By.css('button')).nativeElement;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should be valid when has value', () => {
        component.codeControl.setValue(111111);
        fixture.detectChanges();

        expect(component.codeControl.valid).toBeTrue();
    });

    it('should be disabled when empty', () => {
        button.click();

        expect(mockAuthService.verifyEmail).toHaveBeenCalledTimes(0);
        expect(component.codeControl.valid).toBeFalse();
    });

    it('should set token when valid', () => {
        component.codeControl.setValue(111111);
        fixture.detectChanges();

        const auth: AuthState = {
            token: {
                accessToken: 'test',
                refreshToken: 'test'
            },
            user: {
                email: 'test@test.com',
                firstName: 'test',
                lastName: 'test',
                id: 1
            }
        };
        mockAuthService.verifyEmail.and.returnValue(of(auth));

        button.click();

        expect(mockStore.select).toHaveBeenCalledTimes(1);
        expect(mockAuthService.verifyEmail).toHaveBeenCalledTimes(1);
        expect(mockStore.dispatch).toHaveBeenCalledOnceWith(login({ authState: auth }));
    });

    it('should set error when invalid code', () => {
        component.codeControl.setValue(111111);
        fixture.detectChanges();

        mockAuthService.verifyEmail.and.returnValue(throwError(() => new Error()));

        button.click();

        expect(mockStore.select).toHaveBeenCalledTimes(1);
        expect(mockAuthService.verifyEmail).toHaveBeenCalledTimes(1);
        expect(mockStore.dispatch).toHaveBeenCalledTimes(0);
        expect(component.codeControl.hasError('invalidCode')).toBeTrue();
    });

    it('should call resend code when clicked', () => {
        mockAuthService.resendCode.and.returnValue(of(null));
        component.resendCode();

        expect(mockStore.select).toHaveBeenCalledTimes(1);
        expect(mockAuthService.resendCode).toHaveBeenCalledTimes(1);
    });
});

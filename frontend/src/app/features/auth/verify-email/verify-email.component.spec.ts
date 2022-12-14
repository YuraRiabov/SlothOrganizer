/* eslint-disable no-undef */

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ParamMap, Router } from '@angular/router';
import { of, throwError } from 'rxjs';

import { AuthRoutingModule } from '../auth-routing.module';
import { AuthService } from '@api/auth.service';
import { AuthState } from '@store/states/auth-state';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '@shared/material/material.module';
import { Store } from '@ngrx/store';
import { VerifyEmailComponent } from './verify-email.component';
import { login } from '@store/actions/login-page.actions';

describe('VerifyEmailComponent', () => {
    let component: VerifyEmailComponent;
    let fixture: ComponentFixture<VerifyEmailComponent>;
    let mockAuthService: jasmine.SpyObj<AuthService>;
    let mockStore: jasmine.SpyObj<Store>;
    let router: jasmine.SpyObj<Router>;
    let paramMap: jasmine.SpyObj<ParamMap>;
    let button: HTMLElement;

    const getAuthState = () => ({
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
    });

    beforeEach(async () => {
        mockAuthService = jasmine.createSpyObj(['verifyEmail', 'resendCode']);
        mockStore = jasmine.createSpyObj(['select', 'dispatch']);
        router = jasmine.createSpyObj(['navigate']);
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
                { provide: Router, useValue: router }
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

        const auth: AuthState = getAuthState();
        mockAuthService.verifyEmail.and.returnValue(of(auth));

        button.click();

        expect(mockStore.select).toHaveBeenCalledTimes(1);
        expect(mockAuthService.verifyEmail).toHaveBeenCalledTimes(1);
        expect(mockStore.dispatch).toHaveBeenCalledOnceWith(login({ authState: auth }));
    });

    it('should redirect to root', () => {
        component.codeControl.setValue(111111);
        fixture.detectChanges();

        const auth: AuthState = getAuthState();
        mockAuthService.verifyEmail.and.returnValue(of(auth));

        button.click();

        expect(router.navigate).toHaveBeenCalledOnceWith(['']);
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

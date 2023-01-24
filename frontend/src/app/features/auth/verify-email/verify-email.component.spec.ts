/* eslint-disable no-undef */

import * as authActions from '@store/actions/auth.actions';

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
import { UserCredentialsService } from '@api/user-credentials.service';
import { VerifyEmailComponent } from './verify-email.component';

describe('VerifyEmailComponent', () => {
    let component: VerifyEmailComponent;
    let fixture: ComponentFixture<VerifyEmailComponent>;
    let authService: jasmine.SpyObj<AuthService>;
    let store: jasmine.SpyObj<Store>;
    let router: jasmine.SpyObj<Router>;
    let userCredentialsService: jasmine.SpyObj<UserCredentialsService>;
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
        authService = jasmine.createSpyObj(['sendCode']);
        store = jasmine.createSpyObj(['select', 'dispatch']);
        router = jasmine.createSpyObj(['navigate']);
        userCredentialsService = jasmine.createSpyObj(['verifyEmail']);
        store.select.and.returnValue(of(1));
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
                { provide: AuthService, useValue: authService },
                { provide: UserCredentialsService, useValue: userCredentialsService },
                { provide: Store, useValue: store },
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

        expect(userCredentialsService.verifyEmail).toHaveBeenCalledTimes(0);
        expect(component.codeControl.valid).toBeFalse();
    });

    it('should set token when valid', () => {
        component.codeControl.setValue(111111);
        fixture.detectChanges();

        const auth: AuthState = getAuthState();
        userCredentialsService.verifyEmail.and.returnValue(of(auth));

        button.click();

        expect(store.select).toHaveBeenCalledTimes(1);
        expect(userCredentialsService.verifyEmail).toHaveBeenCalledTimes(1);
        expect(store.dispatch).toHaveBeenCalledOnceWith(authActions.verifyEmail({ authState: auth }));
    });

    it('should redirect to root', () => {
        component.codeControl.setValue(111111);
        fixture.detectChanges();

        const auth: AuthState = getAuthState();
        userCredentialsService.verifyEmail.and.returnValue(of(auth));

        button.click();

        expect(router.navigate).toHaveBeenCalledOnceWith(['']);
    });

    it('should set error when invalid code', () => {
        component.codeControl.setValue(111111);
        fixture.detectChanges();

        userCredentialsService.verifyEmail.and.returnValue(throwError(() => new Error()));

        button.click();

        expect(store.select).toHaveBeenCalledTimes(1);
        expect(userCredentialsService.verifyEmail).toHaveBeenCalledTimes(1);
        expect(store.dispatch).toHaveBeenCalledTimes(0);
        expect(component.codeControl.hasError('invalidCode')).toBeTrue();
    });

    it('should call resend code when clicked', () => {
        authService.sendCode.and.returnValue(of(null));
        component.sendCode();

        expect(store.select).toHaveBeenCalledTimes(1);
        expect(authService.sendCode).toHaveBeenCalledTimes(2);
    });
});

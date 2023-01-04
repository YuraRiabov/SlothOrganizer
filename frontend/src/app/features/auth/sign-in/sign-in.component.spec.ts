/* eslint-disable no-undef */

import * as authActions from '@store/actions/auth.actions';

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { of, throwError } from 'rxjs';

import { AuthRoutingModule } from '../auth-routing.module';
import { AuthService } from '@api/auth.service';
import { AuthState } from '@store/states/auth-state';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '@shared/material/material.module';
import { Router } from '@angular/router';
import { SignInComponent } from './sign-in.component';
import { Store } from '@ngrx/store';

describe('SignInComponent', () => {
    let component: SignInComponent;
    let fixture: ComponentFixture<SignInComponent>;
    let authService: jasmine.SpyObj<AuthService>;
    let store: jasmine.SpyObj<Store>;
    let router: jasmine.SpyObj<Router>;
    let button: HTMLElement;
    const mockAuth: AuthState = {
        user: {
            id: 1,
            email: 'test@test.com',
            firstName: 'test',
            lastName: 'test'
        },
        token: {
            accessToken: 'access',
            refreshToken: 'refresh'
        }
    };

    const setUpValidForm = () => {
        component.signInGroup.get('email')?.setValue('test@test.com');
        component.signInGroup.get('password')?.setValue('aaaaa8a8');
    };

    beforeEach(async () => {
        authService = jasmine.createSpyObj('AuthService', ['signIn']);
        store = jasmine.createSpyObj(['dispatch']);
        router = jasmine.createSpyObj(['navigate']);
        await TestBed.configureTestingModule({
            declarations: [SignInComponent],
            imports: [
                BrowserAnimationsModule,
                CommonModule,
                AuthRoutingModule,
                MaterialModule,
                FormsModule,
                ReactiveFormsModule
            ],
            providers: [
                { provide: AuthService, useValue: authService },
                { provide: Store, useValue: store },
                { provide: Router, useValue: router }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(SignInComponent);
        component = fixture.componentInstance;
        button = fixture.debugElement.query(By.css('button')).nativeElement;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should be valid when valid input', () => {
        setUpValidForm();
        fixture.detectChanges();

        expect(component.signInGroup.valid).toBeTrue();
    });

    it('should be call store and redirect to root when valid input and email verified', () => {
        setUpValidForm();
        fixture.detectChanges();
        authService.signIn.and.returnValue(of(mockAuth));

        button.click();

        expect(component.signInGroup.valid).toBeTrue();
        expect(authService.signIn).toHaveBeenCalledTimes(1);
        expect(store.dispatch).toHaveBeenCalledOnceWith(authActions.login({authState: mockAuth}));
        expect(router.navigate).toHaveBeenCalledOnceWith(['']);
    });

    it('should be call store and redirect to verify email when valid input and email not verified', () => {
        setUpValidForm();
        fixture.detectChanges();
        authService.signIn.and.returnValue(of({ user: mockAuth.user }));

        button.click();

        expect(component.signInGroup.valid).toBeTrue();
        expect(authService.signIn).toHaveBeenCalledTimes(1);
        expect(store.dispatch).toHaveBeenCalledOnceWith(authActions.addUser({user: mockAuth.user}));
        expect(router.navigate).toHaveBeenCalledOnceWith(['auth/verify-email']);
    });

    it('should set login error if invalid email or password', () => {
        setUpValidForm();
        fixture.detectChanges();
        authService.signIn.and.returnValue(throwError(() => new Error()));

        button.click();

        expect(component.signInGroup.valid).toBeFalse();
        expect(authService.signIn).toHaveBeenCalledTimes(1);
        expect(store.dispatch).toHaveBeenCalledTimes(0);
        expect(router.navigate).toHaveBeenCalledTimes(0);

        fixture.detectChanges();
        expect(component.signInGroup.get('email')?.hasError('invalidLogin')).toBeTrue();
        expect(component.signInGroup.get('password')?.hasError('invalidLogin')).toBeTrue();
    });

    it('should be disabled when invalid email', () => {
        setUpValidForm();
        component.signInGroup.get('email')?.setValue('testtest.com');
        fixture.detectChanges();

        button.click();

        expect(component.signInGroup.valid).toBeFalse();
        expect(component.signInGroup.get('email')?.hasError('email')).toBeTrue();
        expect(authService.signIn).toHaveBeenCalledTimes(0);
    });

    it('should be disabled when invalid password pattern', () => {
        setUpValidForm();
        component.signInGroup.get('password')?.setValue('testtestcom');
        fixture.detectChanges();

        button.click();

        expect(component.signInGroup.valid).toBeFalse();
        expect(component.signInGroup.get('password')?.hasError('pattern')).toBeTrue();
        expect(authService.signIn).toHaveBeenCalledTimes(0);
    });

    it('should be disabled when invalid password length', () => {
        setUpValidForm();
        component.signInGroup.get('password')?.setValue('test1');
        fixture.detectChanges();

        button.click();

        expect(component.signInGroup.valid).toBeFalse();
        expect(component.signInGroup.get('password')?.hasError('minlength')).toBeTrue();
        expect(authService.signIn).toHaveBeenCalledTimes(0);
    });

    it('should redirect to sign-up on link click', () => {
        const link = fixture.debugElement.query(By.css('.sign-up-link')).nativeElement;

        link.click();
        fixture.detectChanges();

        expect(router.navigate).toHaveBeenCalledOnceWith(['auth/sign-up']);
    });
});

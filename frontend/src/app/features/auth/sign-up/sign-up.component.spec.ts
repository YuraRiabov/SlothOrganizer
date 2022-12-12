/* eslint-disable no-undef */

import { ActivatedRoute, Router } from '@angular/router';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { of, throwError } from 'rxjs';

import { AuthRoutingModule } from '../auth-routing.module';
import { AuthService } from '@api/auth.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '@shared/material/material.module';
import { SignUpComponent } from './sign-up.component';
import { Store } from '@ngrx/store';
import { User } from '#types/user/user';

describe('SignUpComponent', () => {
    let component: SignUpComponent;
    let fixture: ComponentFixture<SignUpComponent>;
    let mockAuthService : jasmine.SpyObj<AuthService>;
    let mockStore : jasmine.SpyObj<Store>;
    let mockActivatedRoute : jasmine.SpyObj<ActivatedRoute>;
    let mockRouter : jasmine.SpyObj<Router>;
    let button : HTMLElement;

    const setUpValidForm = (component: SignUpComponent) => {
        component.signUpGroup.controls['firstName'].setValue('test');
        component.signUpGroup.controls['lastName'].setValue('test');
        component.signUpGroup.controls['email'].setValue('test@test.com');
        component.signUpGroup.controls['password'].setValue('testtest8');
        component.signUpGroup.controls['repeatPassword'].setValue('testtest8');
    };

    beforeEach(async () => {
        mockAuthService = jasmine.createSpyObj('AuthService', ['signUp']);
        mockStore = jasmine.createSpyObj(['dispatch']);
        mockActivatedRoute = jasmine.createSpyObj(['parent']);
        mockRouter = jasmine.createSpyObj(['navigate']);

        await TestBed.configureTestingModule({
            imports: [
                CommonModule,
                AuthRoutingModule,
                MaterialModule,
                FormsModule,
                ReactiveFormsModule,
                BrowserAnimationsModule
            ],
            declarations: [SignUpComponent],
            providers: [
                { provide: AuthService, useValue: mockAuthService },
                { provide: Store, useValue: mockStore },
                { provide: ActivatedRoute, useValue: mockActivatedRoute },
                { provide: Router, useValue: mockRouter }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(SignUpComponent);
        component = fixture.componentInstance;
        button = fixture.debugElement.query(By.css('button')).nativeElement;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should be valid if valid data', () => {
        setUpValidForm(component);

        expect(component.signUpGroup.valid).toBeTruthy();
    });

    it('should call store on submit if valid data', () => {
        setUpValidForm(component);
        fixture.detectChanges();

        const testUser : User = {
            id: 1,
            firstName: 'test',
            lastName: 'test',
            email: 'test@test.com'
        };

        mockAuthService.signUp.and.returnValue(of(testUser));

        button.click();

        expect(mockRouter.navigate).toHaveBeenCalledOnceWith(['verify-email'], {
            relativeTo: mockActivatedRoute.parent
        });
        expect(mockStore.dispatch).toHaveBeenCalled();
    });

    it('should set error if email taken', () => {
        setUpValidForm(component);
        fixture.detectChanges();

        mockAuthService.signUp.and.returnValue(throwError(() => ({ error: 'Account with this email already exists'})));

        button.click();
        fixture.detectChanges();

        expect(mockAuthService.signUp).toHaveBeenCalled();
        expect(mockRouter.navigate).toHaveBeenCalledTimes(0);
        expect(mockStore.dispatch).toHaveBeenCalledTimes(0);
        expect(component.signUpGroup.get('email')?.hasError('emailTaken')).toBeTrue();
    });

    it('should be disabled if passwords dont match', () => {
        setUpValidForm(component);
        component.signUpGroup.controls['repeatPassword'].setValue('testtest88');
        fixture.detectChanges();

        button.click();
        fixture.detectChanges();

        expect(mockAuthService.signUp).toHaveBeenCalledTimes(0);
        expect(component.signUpGroup.get('repeatPassword')?.hasError('passwordMismatch')).toBeTrue();
    });

    it('should be disabled if any field is empty', () => {
        setUpValidForm(component);
        component.signUpGroup.controls['firstName'].setValue('');
        fixture.detectChanges();

        button.click();
        fixture.detectChanges();

        expect(mockAuthService.signUp).toHaveBeenCalledTimes(0);
        expect(component.signUpGroup.get('firstName')?.hasError('required')).toBeTrue();
    });

    it('should be disabled if password doesnt follow pattern', () => {
        setUpValidForm(component);
        component.signUpGroup.controls['password'].setValue('testtest');
        component.signUpGroup.controls['repeatPassword'].setValue('testtest');
        fixture.detectChanges();

        button.click();
        fixture.detectChanges();

        expect(mockAuthService.signUp).toHaveBeenCalledTimes(0);
        expect(component.signUpGroup.get('password')?.hasError('pattern')).toBeTrue();
    });

    it('should be disabled if name or email invalid', () => {
        setUpValidForm(component);
        component.signUpGroup.controls['firstName'].setValue('t');
        component.signUpGroup.controls['lastName'].setValue('testttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt');
        component.signUpGroup.controls['email'].setValue('testtest.com');
        fixture.detectChanges();

        button.click();
        fixture.detectChanges();

        expect(mockAuthService.signUp).toHaveBeenCalledTimes(0);
        expect(component.signUpGroup.get('firstName')?.hasError('minlength')).toBeTrue();
        expect(component.signUpGroup.get('lastName')?.hasError('maxlength')).toBeTrue();
        expect(component.signUpGroup.get('email')?.hasError('email')).toBeTrue();
    });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { of, throwError } from 'rxjs';

import { AuthRoutingModule } from '../auth-routing.module';
import { AuthService } from '@api/auth.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { EnterEmailComponent } from './enter-email.component';
import { MaterialModule } from '@shared/material/material.module';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { addEmail } from '@store/actions/login-page.actions';

describe('EnterEmailComponent', () => {
    let component: EnterEmailComponent;
    let fixture: ComponentFixture<EnterEmailComponent>;
    let authService: jasmine.SpyObj<AuthService>;
    let store: jasmine.SpyObj<Store>;
    let router: jasmine.SpyObj<Router>;
    let button: HTMLElement;

    const setupEmail = (value: string = 'test@test.com') => component.emailControl.setValue(value);

    beforeEach(async () => {
        authService = jasmine.createSpyObj('AuthService', ['resendCode']);
        store = jasmine.createSpyObj(['dispatch']);
        router = jasmine.createSpyObj(['navigate']);
        authService.resendCode.and.returnValue(of(null));
        await TestBed.configureTestingModule({
            declarations: [EnterEmailComponent],
            imports: [
                CommonModule,
                AuthRoutingModule,
                MaterialModule,
                FormsModule,
                ReactiveFormsModule,
                BrowserAnimationsModule
            ],
            providers: [
                { provide: AuthService, useValue: authService },
                { provide: Store, useValue: store },
                { provide: Router, useValue: router }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(EnterEmailComponent);
        component = fixture.componentInstance;
        button = fixture.debugElement.query(By.css('button')).nativeElement;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should be valid when valid email', () => {
        setupEmail();
        fixture.detectChanges();
        expect(component.emailControl.valid).toBeTrue();
    });

    it('should call store and redirect when valid email', () => {
        setupEmail();
        fixture.detectChanges();

        button.click();

        expect(component.emailControl.valid).toBeTrue();
        expect(authService.resendCode).toHaveBeenCalledTimes(1);
        expect(store.dispatch).toHaveBeenCalledOnceWith(addEmail({email: 'test@test.com'}));
        expect(router.navigate).toHaveBeenCalledTimes(1);
    });

    it('should not call store and redirect when email absent', () => {
        authService.resendCode.and.returnValue(throwError(() => new Error()));
        setupEmail();
        fixture.detectChanges();

        expect(component.emailControl.valid).toBeTrue();
        button.click();

        expect(authService.resendCode).toHaveBeenCalledTimes(1);
        expect(store.dispatch).toHaveBeenCalledTimes(0);
        expect(router.navigate).toHaveBeenCalledTimes(0);

        fixture.detectChanges();
        expect(component.emailControl.hasError('email')).toBeTrue();
    });

    it('should be invalid when empty email', () => {
        setupEmail('');
        fixture.detectChanges();

        button.click();

        expect(component.emailControl.hasError('required')).toBeTrue();
        expect(authService.resendCode).toHaveBeenCalledTimes(0);
        expect(store.dispatch).toHaveBeenCalledTimes(0);
        expect(router.navigate).toHaveBeenCalledTimes(0);
    });

    it('should be invalid when invalid email', () => {
        setupEmail('testtest.com');
        fixture.detectChanges();

        button.click();

        expect(component.emailControl.hasError('email')).toBeTrue();
        expect(authService.resendCode).toHaveBeenCalledTimes(0);
        expect(store.dispatch).toHaveBeenCalledTimes(0);
        expect(router.navigate).toHaveBeenCalledTimes(0);
    });
});
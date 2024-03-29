import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { of, throwError } from 'rxjs';

import { AuthRoutingModule } from '../auth-routing.module';
import { AuthService } from '@api/auth.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { EmailRecoveryComponent } from './enter-email.component';
import { MaterialModule } from '@shared/material/material.module';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';

describe('EmailRecoveryComponent', () => {
    let component: EmailRecoveryComponent;
    let fixture: ComponentFixture<EmailRecoveryComponent>;
    let authService: jasmine.SpyObj<AuthService>;
    let store: jasmine.SpyObj<Store>;
    let router: jasmine.SpyObj<Router>;
    let button: HTMLElement;

    const setupEmail = (value: string = 'test@test.com') => component.emailControl.setValue(value);

    beforeEach(async () => {
        authService = jasmine.createSpyObj('AuthService', ['sendPasswordResetLink']);
        store = jasmine.createSpyObj(['dispatch']);
        router = jasmine.createSpyObj(['navigate']);
        authService.sendPasswordResetLink.and.returnValue(of(null));
        await TestBed.configureTestingModule({
            declarations: [EmailRecoveryComponent],
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

        fixture = TestBed.createComponent(EmailRecoveryComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
        button = fixture.debugElement.query(By.css('button')).nativeElement;
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should be valid when valid email', () => {
        setupEmail();
        fixture.detectChanges();
        expect(component.emailControl.valid).toBeTrue();
    });

    it('should send when valid email', () => {
        setupEmail();
        fixture.detectChanges();

        button.click();

        expect(component.emailControl.valid).toBeTrue();
        expect(authService.sendPasswordResetLink).toHaveBeenCalledTimes(1);
    });

    it('should not send when email absent', () => {
        authService.sendPasswordResetLink.and.returnValue(throwError(() => new Error()));
        setupEmail();
        fixture.detectChanges();

        expect(component.emailControl.valid).toBeTrue();
        button.click();

        expect(authService.sendPasswordResetLink).toHaveBeenCalledTimes(1);

        fixture.detectChanges();
        expect(component.emailControl.hasError('email')).toBeTrue();
    });

    it('should be invalid when empty email', () => {
        setupEmail('');
        fixture.detectChanges();

        button.click();

        expect(component.emailControl.hasError('required')).toBeTrue();
        expect(authService.sendPasswordResetLink).toHaveBeenCalledTimes(0);
    });

    it('should be invalid when invalid email', () => {
        setupEmail('testtest.com');
        fixture.detectChanges();

        button.click();

        expect(component.emailControl.hasError('email')).toBeTrue();
        expect(authService.sendPasswordResetLink).toHaveBeenCalledTimes(0);
    });
});

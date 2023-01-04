import { ActivatedRoute, Router } from '@angular/router';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { map, of } from 'rxjs';

import { AuthRoutingModule } from '../auth-routing.module';
import { AuthService } from '@api/auth.service';
import { AuthState } from '@store/states/auth-state';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '@shared/material/material.module';
import { ResetPasswordComponent } from './reset-password.component';
import { Store } from '@ngrx/store';
import { login } from '@store/actions/auth.actions';

describe('ResetPasswordComponent', () => {
    let component: ResetPasswordComponent;
    let fixture: ComponentFixture<ResetPasswordComponent>;
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
        component.resetPassowordGroup.get('password')?.setValue('testtest8');
        component.resetPassowordGroup.get('repeatPassword')?.setValue('testtest8');
    };
    beforeEach(async () => {
        authService = jasmine.createSpyObj(['resetPassword']);
        router = jasmine.createSpyObj(['navigate']);
        store = jasmine.createSpyObj(['dispatch']);
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
                { provide: AuthService, useValue: authService },
                { provide: Store, useValue: store },
                { provide: Router, useValue: router },
                {
                    provide: ActivatedRoute, useValue: {
                        queryParams: of({ code: '111111', email: 'test@test.com' })
                    }
                }
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
        authService.resetPassword.and.returnValue(of(mockAuth));

        button.click();

        expect(authService.resetPassword).toHaveBeenCalledTimes(1);
        expect(store.dispatch).toHaveBeenCalledOnceWith(login({ authState: mockAuth }));
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

        expect(authService.resetPassword).toHaveBeenCalledTimes(0);
        expect(component.resetPassowordGroup.get('repeatPassword')?.hasError('passwordMismatch')).toBeTrue();
    });
});

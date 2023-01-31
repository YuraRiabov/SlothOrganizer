import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '@shared/material/material.module';
import { ProfileComponent } from './profile.component';
import { ProfileRoutingModule } from '../profile-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { Store } from '@ngrx/store';

describe('ProfileComponent', () => {
    let component: ProfileComponent;
    let fixture: ComponentFixture<ProfileComponent>;
    let store: jasmine.SpyObj<Store>;

    beforeEach(async () => {
        store = jasmine.createSpyObj(['select', 'dispatch']);
        await TestBed.configureTestingModule({
            declarations: [ProfileComponent],
            imports: [
                BrowserAnimationsModule,
                CommonModule,
                ProfileRoutingModule,
                MaterialModule,
                ReactiveFormsModule
            ],
            providers: [
                { provide: Store, useValue: store }
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(ProfileComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

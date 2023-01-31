import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';
import { MaterialModule } from '@shared/material/material.module';
import { Store } from '@ngrx/store';

describe('HeaderComponent', () => {
    let component: HeaderComponent;
    let fixture: ComponentFixture<HeaderComponent>;
    let store: jasmine.SpyObj<Store>;

    beforeEach(async () => {
        store = jasmine.createSpyObj(['select', 'dispatch']);
        await TestBed.configureTestingModule({
            declarations: [HeaderComponent],
            imports: [
                BrowserAnimationsModule,
                CommonModule,
                MaterialModule
            ],
            providers: [
                { provide: Store, useValue: store }
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(HeaderComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

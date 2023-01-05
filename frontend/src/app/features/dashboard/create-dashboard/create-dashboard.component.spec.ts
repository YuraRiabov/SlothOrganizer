import * as dashboardActions from '@store/actions/dashboard.actions';

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { CreateDashboardComponent } from './create-dashboard.component';
import { DashboardRoutingModule } from '../dashboard-routing.module';
import { MaterialModule } from '@shared/material/material.module';
import { Store } from '@ngrx/store';
import { of } from 'rxjs';

describe('CreateDashboardComponent', () => {
    let component: CreateDashboardComponent;
    let fixture: ComponentFixture<CreateDashboardComponent>;
    let store: jasmine.SpyObj<Store>;
    let submitButton: HTMLElement;

    beforeEach(async () => {
        store = jasmine.createSpyObj('Store', ['select', 'dispatch']);
        store.select.and.returnValue(of(1));
        await TestBed.configureTestingModule({
            imports: [
                CommonModule,
                DashboardRoutingModule,
                MaterialModule,
                FormsModule,
                ReactiveFormsModule,
                BrowserAnimationsModule
            ],
            declarations: [CreateDashboardComponent],
            providers: [
                { provide: Store, useValue: store }
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(CreateDashboardComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
        submitButton = fixture.debugElement.query(By.css('.submit-button')).nativeElement;
        spyOn(component.closed, 'emit');
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should be disabled when title empty', () => {
        submitButton.click();

        expect(store.select).toHaveBeenCalledTimes(0);
    });

    it('should call store when title not empty', () => {
        component.dashboardTitleControl.setValue('Title');
        fixture.detectChanges();

        submitButton.click();

        expect(store.select).toHaveBeenCalledTimes(1);
        expect(store.dispatch)
            .toHaveBeenCalledOnceWith(dashboardActions.createDashbaord({
                dashboard: {
                    userId: 1,
                    title: 'Title'
                }
            }));
    });

    it('should call close on cancel click', () => {
        let cancelButton = fixture.debugElement.query(By.css('.cancel-button')).nativeElement;

        cancelButton.click();

        expect(component.closed.emit).toHaveBeenCalledTimes(1);
    });
});

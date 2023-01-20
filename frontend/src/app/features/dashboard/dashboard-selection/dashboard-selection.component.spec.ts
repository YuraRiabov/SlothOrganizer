import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from '../dashboard-routing.module';
import { DashboardSelectionComponent } from './dashboard-selection.component';
import { MaterialModule } from '@shared/material/material.module';

describe('DashboardSelectionComponent', () => {
    let component: DashboardSelectionComponent;
    let fixture: ComponentFixture<DashboardSelectionComponent>;
    let submitButton: HTMLElement;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                CommonModule,
                DashboardRoutingModule,
                MaterialModule,
                FormsModule,
                ReactiveFormsModule,
                BrowserAnimationsModule
            ],
            declarations: [DashboardSelectionComponent]
        })
            .compileComponents();

        fixture = TestBed.createComponent(DashboardSelectionComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
        spyOn(component, 'createDashboard');
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should be disabled when title empty', () => {
        component.creating = true;
        fixture.detectChanges();

        submitButton = fixture.debugElement.query(By.css('.submit-button')).nativeElement;
        submitButton.click();

        expect(component.createDashboard).toHaveBeenCalledTimes(0);
    });

    it('should call store when title not empty', () => {
        component.creating = true;
        fixture.detectChanges();

        component.dashboardTitleControl.setValue('Title');
        fixture.detectChanges();

        submitButton = fixture.debugElement.query(By.css('.submit-button')).nativeElement;
        submitButton.click();

        expect(component.createDashboard).toHaveBeenCalledTimes(1);
    });

    it('should close creation on cancel click', () => {
        component.creating = true;
        fixture.detectChanges();
        const cancelButton = fixture.debugElement.query(By.css('.cancel-button')).nativeElement;
        cancelButton.click();

        expect(component.creating).toBeFalse();
    });
});

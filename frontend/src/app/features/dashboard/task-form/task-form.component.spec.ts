import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { endOfDay, startOfDay } from 'date-fns';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from '../dashboard-routing.module';
import { MaterialModule } from '@shared/material/material.module';
import { Store } from '@ngrx/store';
import { TaskFormComponent } from './task-form.component';
import { TaskRepeatingPeriod } from '#types/dashboard/tasks/enums/task-repeating-period';
import { of } from 'rxjs';

describe('TaskFormComponent', () => {
    let component: TaskFormComponent;
    let fixture: ComponentFixture<TaskFormComponent>;
    let store: jasmine.SpyObj<Store>;
    let saveButton: HTMLElement;
    let initialValue = {
        dashboardId: 1,
        title: '',
        description: '',
        repeatingPeriod: TaskRepeatingPeriod.None,
        start: startOfDay(new Date()),
        end: endOfDay(new Date()),
    };

    const buildValidForm = () => {
        component.taskForm.get('title')?.setValue('Title');
        component.taskForm.get('description')?.setValue('description');
        component.taskForm.get('startDate')?.setValue(new Date(2023, 0, 1));
        component.taskForm.get('endDate')?.setValue(new Date(2023, 0, 4));
        component.taskForm.get('startTimeCheckbox')?.setValue(true);
        component.taskForm.get('endTimeCheckbox')?.setValue(true);
        component.taskForm.get('repeatingPeriod')?.setValue(TaskRepeatingPeriod.Week);
        component.taskForm.get('endRepeating')?.setValue(new Date(2023, 1, 4));
    };

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
            declarations: [TaskFormComponent],
            providers: [
                { provide: Store, useValue: store}
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(TaskFormComponent);
        component = fixture.componentInstance;
        component.initialValue = initialValue;
        fixture.detectChanges();
        saveButton = fixture.debugElement.query(By.css('.save-button')).nativeElement;
        spyOn(component.submitted, 'emit');
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should be valid when valid data', () => {
        buildValidForm();
        fixture.detectChanges();

        expect(component.taskForm.valid).toBeTrue();
    });

    it('should call createTask when valid data and saved', () => {
        buildValidForm();
        fixture.detectChanges();

        saveButton.click();

        expect(component.submitted.emit).toHaveBeenCalledTimes(1);
    });

    it('should have error when start after end', () => {
        buildValidForm();
        component.taskForm.get('startDate')?.setValue(new Date(2023, 0, 5));
        fixture.detectChanges();

        expect(component.taskForm.valid).toBeFalse();
        expect(component.taskForm.get('startDate')?.hasError('startBeforeEnd')).toBeTrue();
        expect(component.taskForm.get('endDate')?.hasError('startBeforeEnd')).toBeTrue();
    });

    it('should have error when repeating period shorter than task', () => {
        buildValidForm();
        component.taskForm.get('repeatingPeriod')?.setValue(TaskRepeatingPeriod.Day);
        fixture.detectChanges();

        expect(component.taskForm.valid).toBeFalse();
        expect(component.taskForm.get('repeatingPeriod')?.hasError('shortRepeatingPeriod')).toBeTrue();
    });

    it('should have error when end repeating is earlier than task end', () => {
        buildValidForm();
        component.taskForm.get('endRepeating')?.setValue(new Date(2023, 0, 2));
        fixture.detectChanges();

        expect(component.taskForm.valid).toBeFalse();
        expect(component.taskForm.get('endRepeating')?.hasError('endBeforeEndRepeating')).toBeTrue();
    });

    it('should be invalid when title length is invalid', () => {
        buildValidForm();
        component.taskForm.get('title')?.setValue('t');
        fixture.detectChanges();

        expect(component.taskForm.valid).toBeFalse();
    });
});

import * as dashboardActions from '@store/actions/dashboard.actions';

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from '../dashboard-routing.module';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '@shared/material/material.module';
import { Store } from '@ngrx/store';
import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { TaskInfoComponent } from './task-info.component';
import { of } from 'rxjs';

describe('TaskInfoComponent', () => {
    let component: TaskInfoComponent;
    let fixture: ComponentFixture<TaskInfoComponent>;
    let store: jasmine.SpyObj<Store>;
    const taskBlock: TaskBlock = {
        task: {
            title: 'title',
            id: 1,
            dashboardId: 1,
            description: 'title',
            taskCompletions: []
        },
        taskCompletion: {
            start: new Date(2022, 11, 16, 8),
            end: new Date(2022, 13, 1, 0),
            id: 1,
            taskId: 1,
            isSuccessful: false
        },
        color: 'blue'
    };

    beforeEach(async () => {
        store = jasmine.createSpyObj('Store', ['select', 'dispatch']);
        store.select.and.returnValue(of(taskBlock));
        await TestBed.configureTestingModule({
            declarations: [TaskInfoComponent],
            imports: [
                CommonModule,
                DashboardRoutingModule,
                MaterialModule,
                FormsModule
            ],
            providers: [
                { provide: Store, useValue: store }
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(TaskInfoComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
        spyOn(component.switchToEdit, 'emit');
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should dispatch delete when delete clicked', () => {
        let button = fixture.debugElement.query(By.css('.delete-button')).nativeElement;

        button.click();

        expect(store.dispatch).toHaveBeenCalledOnceWith(dashboardActions.deleteTask());
    });

    it('should dispatch mark as completed when mark as completed clicked', () => {
        let button = fixture.debugElement.query(By.css('.mark-completed-button')).nativeElement;

        button.click();

        expect(store.dispatch).toHaveBeenCalledOnceWith(dashboardActions.markTaskCompleted());
    });

    it('should change to edit when edit clicked', () => {
        let button = fixture.debugElement.query(By.css('.edit-button')).nativeElement;

        button.click();

        expect(component.switchToEdit.emit).toHaveBeenCalledTimes(1);
    });
});

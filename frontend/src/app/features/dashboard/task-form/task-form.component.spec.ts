import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { DashboardEffects } from '@store/effects/dashboard/dashboard.effects';
import { DashboardRoutingModule } from '../dashboard-routing.module';
import { EffectsModule } from '@ngrx/effects';
import { MaterialModule } from '@shared/material/material.module';
import { Store } from '@ngrx/store';
import { TaskFormComponent } from './task-form.component';
import { TasksEffects } from '@store/effects/dashboard/tasks.effects';

describe('TaskFormComponent', () => {
    let component: TaskFormComponent;
    let fixture: ComponentFixture<TaskFormComponent>;
    let store: jasmine.SpyObj<Store>;

    beforeEach(async () => {
        store = jasmine.createSpyObj('Store', ['select', 'dispatch']);
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
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

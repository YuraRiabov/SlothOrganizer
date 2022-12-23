import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from '../dashboard-routing.module';
import { ExceedingTasksComponent } from './exceeding-tasks.component';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '@shared/material/material.module';

describe('ExceedingTasksComponent', () => {
    let component: ExceedingTasksComponent;
    let fixture: ComponentFixture<ExceedingTasksComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [ExceedingTasksComponent],
            imports: [
                CommonModule,
                DashboardRoutingModule,
                MaterialModule,
                FormsModule
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(ExceedingTasksComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

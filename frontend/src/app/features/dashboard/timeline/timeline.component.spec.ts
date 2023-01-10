import { ComponentFixture, TestBed } from '@angular/core/testing';

import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from '../dashboard-routing.module';
import { ExceedingTasksComponent } from '../exceeding-tasks/exceeding-tasks.component';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '@shared/material/material.module';
import { Store } from '@ngrx/store';
import { Task } from '#types/dashboard/tasks/task';
import { TimelineComponent } from './timeline.component';
import { TimelineScale } from '#types/dashboard/timeline/enums/timeline-scale';
import { of } from 'rxjs';
import { selectTasks } from '@store/selectors/dashboard.selectors';

describe('TimelineComponent', () => {
    let component: TimelineComponent;
    let fixture: ComponentFixture<TimelineComponent>;
    let store: jasmine.SpyObj<Store>;
    const mockTask: Task = {
        title: 'title',
        id: 1,
        dashboardId: 1,
        description: 'title',
        taskCompletions: [
            {
                start: new Date(2022, 11, 16, 8),
                end: new Date(2022, 13, 1, 0),
                id: 1,
                taskId: 1,
                isSuccessful: false
            },
            {
                start: new Date(2022, 12, 16, 8),
                end: new Date(2022, 12, 22, 0),
                id: 2,
                taskId: 1,
                isSuccessful: false
            },
            {
                start: new Date(2022, 12, 16, 8),
                end: new Date(2022, 12, 16, 11),
                id: 3,
                taskId: 1,
                isSuccessful: false
            },
            {
                start: new Date(2022, 12, 16, 15),
                end: new Date(2022, 12, 16, 17),
                id: 4,
                taskId: 1,
                isSuccessful: false
            },
            {
                start: new Date(2022, 12, 16, 10),
                end: new Date(2022, 12, 16, 23),
                id: 5,
                taskId: 1,
                isSuccessful: false
            },
            {
                start: new Date(2022, 12, 16, 12),
                end: new Date(2022, 12, 16, 18),
                id: 6,
                taskId: 1,
                isSuccessful: false
            },
            {
                start: new Date(2022, 12, 16, 13),
                end: new Date(2022, 12, 16, 20),
                id: 7,
                taskId: 1,
                isSuccessful: false
            },
            {
                start: new Date(2022, 12, 16, 14),
                end: new Date(2022, 12, 16, 15),
                id: 8,
                taskId: 1,
                isSuccessful: false
            },
            {
                start: new Date(2022, 12, 16, 14),
                end: new Date(2022, 12, 16, 15),
                id: 9,
                taskId: 1,
                isSuccessful: false
            }
        ]
    };

    beforeEach(async () => {
        store = jasmine.createSpyObj('Store', ['select', 'dispatch']);
        store.select.and.returnValue(of([mockTask]));
        await TestBed.configureTestingModule({
            declarations: [TimelineComponent, ExceedingTasksComponent],
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

        fixture = TestBed.createComponent(TimelineComponent);
        component = fixture.componentInstance;
        component.scale = TimelineScale.Day;
        component.date = new Date(2022, 12, 16, 12);
        fixture.detectChanges();
        spyOn(component.blockClicked, 'emit');
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should contain 7 blocks when scale day', () => {
        let blocks = fixture.debugElement.queryAll(By.css('.task-block'));
        expect(blocks.length === 7).toBeTrue();
    });

    it('should contain 5 blocks when scale week', () => {
        component.scale = TimelineScale.Week;
        fixture.detectChanges();
        let blocks = fixture.debugElement.queryAll(By.css('.task-block'));
        expect(blocks.length === 5).toBeTrue();
    });

    it('should contain 2 blocks when scale month', () => {
        component.scale = TimelineScale.Month;
        fixture.detectChanges();
        let blocks = fixture.debugElement.queryAll(By.css('.task-block'));
        expect(blocks.length === 2).toBeTrue();
    });

    it('should contain 1 block when scale year', () => {
        component.scale = TimelineScale.Year;
        fixture.detectChanges();
        let blocks = fixture.debugElement.queryAll(By.css('.task-block'));
        expect(blocks.length === 1).toBeTrue();
    });

    it('should call event when block clicked', () => {
        component.scale = TimelineScale.Year;
        fixture.detectChanges();

        let block = fixture.debugElement.query(By.css('.task-block')).nativeElement;
        block.click();
        fixture.detectChanges();

        expect(component.blockClicked.emit).toHaveBeenCalledTimes(1);
        expect(store.dispatch).toHaveBeenCalledTimes(1);
    });
});

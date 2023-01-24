import { ComponentFixture, TestBed } from '@angular/core/testing';

import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from '../dashboard-routing.module';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '@shared/material/material.module';
import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { TimelineComponent } from './timeline.component';
import { TimelineScale } from '#types/dashboard/timeline/enums/timeline-scale';

describe('TimelineComponent', () => {
    let component: TimelineComponent;
    let fixture: ComponentFixture<TimelineComponent>;
    const mockTasks: TaskBlock[] = [
        {
            start: new Date(2022, 11, 16, 8),
            end: new Date(2022, 13, 1, 0),
            title: 'month block'
        },
        {
            start: new Date(2022, 12, 16, 8),
            end: new Date(2022, 12, 22, 0),
            title: 'big block'
        },
        {
            start: new Date(2022, 12, 16, 8),
            end: new Date(2022, 12, 16, 11),
            title: 'first block'
        },
        {
            start: new Date(2022, 12, 16, 15),
            end: new Date(2022, 12, 16, 17),
            title: 'second block'
        },
        {
            start: new Date(2022, 12, 16, 10),
            end: new Date(2022, 12, 16, 23),
            title: 'third block'
        },
        {
            start: new Date(2022, 12, 16, 12),
            end: new Date(2022, 12, 16, 18),
            title: 'fourth block'
        },
        {
            start: new Date(2022, 12, 16, 13),
            end: new Date(2022, 12, 16, 20),
            title: 'fifth block'
        },
        {
            start: new Date(2022, 12, 16, 14),
            end: new Date(2022, 12, 16, 15),
            title: 'sixth block'
        },
        {
            start: new Date(2022, 12, 16, 14),
            end: new Date(2022, 12, 16, 15),
            title: 'sixth block'
        },
        {
            start: new Date(2022, 12, 16, 14),
            end: new Date(2022, 12, 16, 15),
            title: 'sixth block'
        },
        {
            start: new Date(2022, 12, 16, 14),
            end: new Date(2022, 12, 16, 15),
            title: 'sixth block'
        },
        {
            start: new Date(2022, 12, 16, 14),
            end: new Date(2022, 12, 16, 15),
            title: 'sixth block'
        },
        {
            start: new Date(2022, 12, 16, 10),
            end: new Date(2022, 12, 16, 12, 30),
            title: 'sixth block'
        }
    ];

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [TimelineComponent],
            imports: [
                CommonModule,
                DashboardRoutingModule,
                MaterialModule,
                FormsModule
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(TimelineComponent);
        component = fixture.componentInstance;
        component.tasks = mockTasks;
        component.scale = TimelineScale.Day;
        component.date = new Date(2022, 12, 16, 12);
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should contain 8 blocks when scale day', () => {
        const blocks = fixture.debugElement.queryAll(By.css('.task-block'));
        expect(blocks.length === 8).toBeTrue();
    });

    it('should contain 5 blocks when scale week', () => {
        component.scale = TimelineScale.Week;
        fixture.detectChanges();
        const blocks = fixture.debugElement.queryAll(By.css('.task-block'));
        expect(blocks.length === 5).toBeTrue();
    });

    it('should contain 2 blocks when scale month', () => {
        component.scale = TimelineScale.Month;
        fixture.detectChanges();
        const blocks = fixture.debugElement.queryAll(By.css('.task-block'));
        expect(blocks.length === 2).toBeTrue();
    });

    it('should contain 1 block when scale year', () => {
        component.scale = TimelineScale.Year;
        fixture.detectChanges();
        const blocks = fixture.debugElement.queryAll(By.css('.task-block'));
        expect(blocks.length === 1).toBeTrue();
    });
});

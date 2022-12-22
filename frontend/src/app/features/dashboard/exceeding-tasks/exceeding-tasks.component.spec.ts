import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExceedingTasksComponent } from './exceeding-tasks.component';

describe('ExceedingTasksComponent', () => {
    let component: ExceedingTasksComponent;
    let fixture: ComponentFixture<ExceedingTasksComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [ExceedingTasksComponent]
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

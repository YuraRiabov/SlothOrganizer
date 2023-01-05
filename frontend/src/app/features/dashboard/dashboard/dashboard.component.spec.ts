import { ComponentFixture, TestBed } from '@angular/core/testing';

import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { CreateDashboardComponent } from '../create-dashboard/create-dashboard.component';
import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from '../dashboard-routing.module';
import { DashboardSidebarComponent } from '../dashboard-sidebar/dashboard-sidebar.component';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '@shared/material/material.module';
import { Store } from '@ngrx/store';
import { TaskFormComponent } from '../task-form/task-form.component';
import { TimelineComponent } from '../timeline/timeline.component';
import { of } from 'rxjs';

describe('DashboardComponent', () => {
    let component: DashboardComponent;
    let fixture: ComponentFixture<DashboardComponent>;
    let store: jasmine.SpyObj<Store>;
    let dashboards: Dashboard[] = [
        {
            id: 1,
            userId: 1,
            title: 'Routine'
        },
        {
            id: 2,
            userId: 1,
            title: 'Work'
        }
    ];

    const getByClass = (className: string) => fixture.debugElement.query(By.css('.' + className))?.nativeElement;

    beforeEach(async () => {
        store = jasmine.createSpyObj('Store', ['select', 'dispatch']);
        store.select.and.returnValue(of(dashboards));
        await TestBed.configureTestingModule({
            declarations: [DashboardComponent, TimelineComponent],
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

        fixture = TestBed.createComponent(DashboardComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it ('should open sidebar on new click', () => {
        let newButton = getByClass('create-task-button');

        let sidebar = getByClass('sidebar');
        let blur = getByClass('blur');

        expect(sidebar).toBeFalsy();
        expect(blur).toBeFalsy();

        newButton.click();
        fixture.detectChanges();

        sidebar = getByClass('sidebar');
        blur = getByClass('blur');

        expect(sidebar).toBeTruthy();
        expect(blur).toBeTruthy();
    });

    it ('should open new dashboard on button click', () => {
        let newButton = getByClass('new-dashboard-button');

        let newDashboard = getByClass('new-dashboard');

        expect(newDashboard).toBeFalsy();

        newButton.click();
        fixture.detectChanges();

        newDashboard = getByClass('new-dashboard');

        expect(newDashboard).toBeTruthy();
    });
});

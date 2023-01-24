import * as dashboardActions from '@store/actions/dashboard.actions';

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { By } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { Dashboard } from '#types/dashboard/dashboard/dashboard';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from '../dashboard-routing.module';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '@shared/material/material.module';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';
import { Store } from '@ngrx/store';
import { TimelineComponent } from '../timeline/timeline.component';
import { of } from 'rxjs';

describe('DashboardComponent', () => {
    let component: DashboardComponent;
    let fixture: ComponentFixture<DashboardComponent>;
    let store: jasmine.SpyObj<Store>;
    const dashboards: Dashboard[] = [
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
        const newButton = getByClass('create-task-button');

        newButton.click();
        fixture.detectChanges();

        expect(store.dispatch).toHaveBeenCalledWith(dashboardActions.openSidebar({ sidebarType: SidebarType.Create }));
    });
});

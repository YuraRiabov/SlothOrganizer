import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from '../dashboard-routing.module';
import { MaterialModule } from '@shared/material/material.module';
import { Store } from '@ngrx/store';
import { UpdateTaskComponent } from './update-task.component';
import { of } from 'rxjs';

describe('UpdateTaskComponent', () => {
    let component: UpdateTaskComponent;
    let fixture: ComponentFixture<UpdateTaskComponent>;
    let store: jasmine.SpyObj<Store>;

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
            declarations: [UpdateTaskComponent],
            providers: [
                { provide: Store, useValue: store}
            ]
        })
            .compileComponents();
        fixture = TestBed.createComponent(UpdateTaskComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

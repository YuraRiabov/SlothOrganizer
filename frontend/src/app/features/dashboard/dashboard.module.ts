import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardEffects } from '@store/effects/dashboard/dashboard.effects';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { EffectsModule } from '@ngrx/effects';
import { ExceedingTasksComponent } from './exceeding-tasks/exceeding-tasks.component';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { TasksEffects } from '@store/effects/dashboard/tasks.effects';
import { TimelineComponent } from './timeline/timeline.component';
import { dashboardReducer } from '@store/reducers/dashboard.reducers';

@NgModule({
    declarations: [
        DashboardComponent,
        TimelineComponent,
        ExceedingTasksComponent
    ],
    imports: [
        CommonModule,
        DashboardRoutingModule,
        MaterialModule,
        FormsModule,
        StoreModule.forFeature('Dashboard', [dashboardReducer]),
        EffectsModule.forFeature([DashboardEffects, TasksEffects])
    ]
})
export class DashboardModule { }

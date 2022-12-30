import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CommonModule } from '@angular/common';
import { CreateDashboardComponent } from './create-dashboard/create-dashboard.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardEffects } from '@store/effects/dashboard/dashboard.effects';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { EffectsModule } from '@ngrx/effects';
import { ExceedingTasksComponent } from './exceeding-tasks/exceeding-tasks.component';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { TasksEffects } from '@store/effects/dashboard/tasks.effects';
import { TimelineComponent } from './timeline/timeline.component';

@NgModule({
    declarations: [
        DashboardComponent,
        TimelineComponent,
        ExceedingTasksComponent,
        CreateDashboardComponent
    ],
    imports: [
        CommonModule,
        DashboardRoutingModule,
        MaterialModule,
        FormsModule,
        ReactiveFormsModule,
        EffectsModule.forFeature([DashboardEffects, TasksEffects])
    ]
})
export class DashboardModule { }

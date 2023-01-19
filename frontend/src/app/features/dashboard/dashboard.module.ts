import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardEffects } from '@store/effects/dashboard/dashboard.effects';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardSelectionComponent } from './dashboard-selection/dashboard-selection.component';
import { DashboardSidebarComponent } from './dashboard-sidebar/dashboard-sidebar.component';
import { EffectsModule } from '@ngrx/effects';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { TaskFormComponent } from './task-form/task-form.component';
import { TasksEffects } from '@store/effects/dashboard/tasks.effects';
import { TimelineComponent } from './timeline/timeline.component';

@NgModule({
    declarations: [
        DashboardComponent,
        TimelineComponent,
        DashboardSidebarComponent,
        TaskFormComponent,
        DashboardSelectionComponent
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

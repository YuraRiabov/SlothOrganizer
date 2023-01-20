import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardEffects } from '@store/effects/dashboard/dashboard.effects';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardSelectionComponent } from './dashboard-selection/dashboard-selection.component';
import { EffectsModule } from '@ngrx/effects';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/components/shared/shared.module';
import { TaskCompletionEffects } from '@store/effects/dashboard/task-completion.effects';
import { TaskFormComponent } from './task-form/task-form.component';
import { TaskInfoComponent } from './task-info/task-info.component';
import { TasksEffects } from '@store/effects/dashboard/tasks.effects';
import { TimelineComponent } from './timeline/timeline.component';

@NgModule({
    declarations: [
        DashboardComponent,
        TimelineComponent,
        TaskFormComponent,
        TaskInfoComponent,
        DashboardSelectionComponent
    ],
    imports: [
        CommonModule,
        DashboardRoutingModule,
        MaterialModule,
        SharedModule,
        FormsModule,
        ReactiveFormsModule,
        EffectsModule.forFeature([DashboardEffects, TasksEffects, TaskCompletionEffects])
    ]
})
export class DashboardModule { }

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CommonModule } from '@angular/common';
import { CreateDashboardComponent } from './create-dashboard/create-dashboard.component';
import { CreateTaskComponent } from './create-task/create-task.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardEffects } from '@store/effects/dashboard/dashboard.effects';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardSidebarComponent } from './dashboard-sidebar/dashboard-sidebar.component';
import { EffectsModule } from '@ngrx/effects';
import { ExceedingTasksComponent } from './exceeding-tasks/exceeding-tasks.component';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { TaskCompletionEffects } from '@store/effects/dashboard/task-completion.effects';
import { TaskFormComponent } from './task-form/task-form.component';
import { TaskInfoComponent } from './task-info/task-info.component';
import { TasksEffects } from '@store/effects/dashboard/tasks.effects';
import { TimelineComponent } from './timeline/timeline.component';
import { UpdateTaskComponent } from './update-task/update-task.component';

@NgModule({
    declarations: [
        DashboardComponent,
        TimelineComponent,
        ExceedingTasksComponent,
        CreateDashboardComponent,
        DashboardSidebarComponent,
        TaskFormComponent,
        TaskInfoComponent,
        CreateTaskComponent,
        UpdateTaskComponent
    ],
    imports: [
        CommonModule,
        DashboardRoutingModule,
        MaterialModule,
        FormsModule,
        ReactiveFormsModule,
        EffectsModule.forFeature([DashboardEffects, TasksEffects, TaskCompletionEffects])
    ]
})
export class DashboardModule { }

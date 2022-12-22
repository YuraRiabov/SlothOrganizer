import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { ExceedingTasksComponent } from './exceeding-tasks/exceeding-tasks.component';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { TimelineComponent } from './timeline/timeline.component';

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
        FormsModule
    ]
})
export class DashboardModule { }

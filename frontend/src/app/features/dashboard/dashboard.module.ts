import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { TimelineComponent } from './timeline/timeline.component';

@NgModule({
    declarations: [
        DashboardComponent,
        TimelineComponent
    ],
    imports: [
        CommonModule,
        DashboardRoutingModule,
        MaterialModule
    ]
})
export class DashboardModule { }

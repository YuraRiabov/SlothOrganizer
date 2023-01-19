import { CommonModule } from '@angular/common';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { SidebarComponent } from './sidebar/sidebar.component';

@NgModule({
    declarations: [
        SidebarComponent
    ],
    imports: [
        CommonModule,
        MaterialModule
    ],
    exports: [
        SidebarComponent
    ]
})
export class SharedModule { }

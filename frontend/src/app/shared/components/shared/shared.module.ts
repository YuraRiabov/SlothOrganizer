import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { SidebarComponent } from './sidebar/sidebar.component';

@NgModule({
    declarations: [
        SidebarComponent,
        HeaderComponent
    ],
    imports: [
        CommonModule,
        MaterialModule
    ],
    exports: [
        SidebarComponent,
        HeaderComponent
    ]
})
export class SharedModule { }

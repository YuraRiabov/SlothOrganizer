import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {MatSelectModule} from '@angular/material/select';
import { NgModule } from '@angular/core';

@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        MatInputModule,
        MatButtonModule,
        MatProgressSpinnerModule,
        MatIconModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatSelectModule,
        MatCheckboxModule
    ],
    exports: [
        MatInputModule,
        MatButtonModule,
        MatProgressSpinnerModule,
        MatIconModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatSelectModule,
        MatCheckboxModule
    ]
})
export class MaterialModule { }

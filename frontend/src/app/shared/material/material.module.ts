import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { NgModule } from '@angular/core';

@NgModule({
    declarations: [],
    imports: [CommonModule, MatInputModule, MatButtonModule, MatProgressSpinnerModule, MatIconModule],
    exports: [MatInputModule, MatButtonModule, MatProgressSpinnerModule, MatIconModule]
})
export class MaterialModule {}

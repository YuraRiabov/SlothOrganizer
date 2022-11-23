import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { NgModule } from '@angular/core';

@NgModule({
  declarations: [],
  imports: [CommonModule, MatInputModule, MatButtonModule],
  exports: [MatInputModule, MatButtonModule]
})
export class MaterialModule {}

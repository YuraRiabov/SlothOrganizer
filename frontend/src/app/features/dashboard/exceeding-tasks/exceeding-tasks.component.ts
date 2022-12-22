import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

import { Task } from '#types/tasks/task';

@Component({
    selector: 'app-exceeding-tasks',
    templateUrl: './exceeding-tasks.component.html',
    styleUrls: ['./exceeding-tasks.component.sass']
})
export class ExceedingTasksComponent {

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: { tasks: Task[] },
        public dialogRef: MatDialogRef<ExceedingTasksComponent>
    ) { }

}
import { Component, EventEmitter, OnInit, Output } from '@angular/core';

import { BaseComponent } from '@shared/components/base/base.component';
import { Store } from '@ngrx/store';
import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { TaskCompletion } from '#types/dashboard/tasks/task-completion';
import { selectChosenTaskBlock } from '@store/selectors/dashboard.selectors';

@Component({
    selector: 'so-task-info',
    templateUrl: './task-info.component.html',
    styleUrls: ['./task-info.component.sass']
})
export class TaskInfoComponent extends BaseComponent implements OnInit {
    public taskBlock!: TaskBlock;

    @Output() public goBack = new EventEmitter();

    constructor(private store: Store) {
        super();
    }

    ngOnInit(): void {
        this.store.select(selectChosenTaskBlock)
            .pipe(this.untilDestroyed)
            .subscribe(taskBlock => this.taskBlock = taskBlock);
    }

    public getBlockStatus() {
        if (this.taskBlock.taskCompletion.isSuccessful) {
            return 'Completed';
        }
        if (this.taskBlock.taskCompletion.end < new Date()) {
            return 'Failed';
        }
        if (this.taskBlock.taskCompletion.start > new Date()) {
            return 'To do';
        }
        return 'In progress';
    }
}

import * as dashboardActions from '@store/actions/dashboard.actions';

import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { selectChosenTaskBlock, selectChosenTaskRepeatingEnd } from '@store/selectors/dashboard.selectors';

import { BaseComponent } from '@shared/components/base/base.component';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { Store } from '@ngrx/store';
import { TaskRepeatingPeriod } from '#types/dashboard/tasks/enums/task-repeating-period';

@Component({
    selector: 'so-update-task',
    templateUrl: './update-task.component.html',
    styleUrls: ['./update-task.component.sass']
})
export class UpdateTaskComponent extends BaseComponent implements OnInit {
    public taskToUpdate!: NewTask;
    @Output() public goBack = new EventEmitter<void>();

    constructor(private store: Store) {
        super();
    }

    ngOnInit(): void {
        this.store.select(selectChosenTaskBlock)
            .pipe(this.untilDestroyed)
            .subscribe(taskBlock => {
                this.taskToUpdate = {
                    ...this.taskToUpdate,
                    dashboardId: -1,
                    title: taskBlock.task.title,
                    description: taskBlock.task.description,
                    start: taskBlock.taskCompletion.start,
                    end: taskBlock.taskCompletion.end,
                    repeatingPeriod: TaskRepeatingPeriod.None
                };
            });

        this.store.select(selectChosenTaskRepeatingEnd)
            .pipe(this.untilDestroyed)
            .subscribe(endRepeating => this.taskToUpdate = { ...this.taskToUpdate, endRepeating });
    }

    public updateTask(task: NewTask): void {
        this.store.dispatch(dashboardActions.editTask({ task }));
        selectChosenTaskBlock.release();
        selectChosenTaskRepeatingEnd.release();
        this.goBack.emit();
    }

}

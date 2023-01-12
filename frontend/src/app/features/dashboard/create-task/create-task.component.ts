import * as dashboardActions from '@store/actions/dashboard.actions';

import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { endOfDay, startOfDay } from 'date-fns';

import { BaseComponent } from '@shared/components/base/base.component';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { Store } from '@ngrx/store';
import { TaskRepeatingPeriod } from '#types/dashboard/tasks/enums/task-repeating-period';
import { selectChosenDashboardId } from '@store/selectors/dashboard.selectors';

@Component({
    selector: 'so-create-task',
    templateUrl: './create-task.component.html',
    styleUrls: ['./create-task.component.sass']
})
export class CreateTaskComponent extends BaseComponent implements OnInit {
    public defaultTask: NewTask;
    @Output() public goBack = new EventEmitter<void>();

    constructor(private store: Store) {
        super();
        this.defaultTask = {
            dashboardId: -1,
            title: '',
            description: '',
            repeatingPeriod: TaskRepeatingPeriod.None,
            start: startOfDay(new Date()),
            end: endOfDay(new Date()),
        };
    }

    ngOnInit(): void {
        this.store.select(selectChosenDashboardId)
            .pipe(this.untilDestroyed)
            .subscribe(id => this.defaultTask.dashboardId = id);
    }

    public createTask(newTask: NewTask) {
        this.store.dispatch(dashboardActions.createTask({ newTask }));
        this.goBack.emit();
    }
}


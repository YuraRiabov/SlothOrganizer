import * as dashboardActions from '@store/actions/dashboard.actions';
import * as taskActions from '@store/actions/task.actions';

import { Component, OnInit } from '@angular/core';

import { BaseComponent } from '@shared/components/base/base.component';
import { Observable } from 'rxjs';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';
import { Store } from '@ngrx/store';
import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { TaskStatus } from '#types/dashboard/timeline/enums/task-status';
import { selectChosenTaskBlock } from '@store/selectors/task.selectors';

@Component({
    selector: 'so-task-info',
    templateUrl: './task-info.component.html',
    styleUrls: ['./task-info.component.sass']
})
export class TaskInfoComponent extends BaseComponent implements OnInit {
    public readonly TaskStatus = TaskStatus;
    public taskBlock$?: Observable<TaskBlock>;

    constructor(private store: Store) {
        super();
    }

    ngOnInit(): void {
        this.taskBlock$ = this.store.select(selectChosenTaskBlock);
    }

    public close(): void {
        this.store.dispatch(dashboardActions.closeSidebar());
    }

    public markAsCompleted(): void {
        this.store.dispatch(taskActions.markCompleted());
    }

    public delete(): void {
        this.store.dispatch(taskActions.deleteTask());
    }

    public edit(): void {
        this.store.dispatch(dashboardActions.openSidebar({ sidebarType: SidebarType.Edit }));
    }
}

import * as dashboardActions from '@store/actions/dashboard.actions';

import { Component, OnInit } from '@angular/core';

import { BaseComponent } from '@shared/components/base/base.component';
import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { selectChosenTaskBlock } from '@store/selectors/dashboard.selectors';

@Component({
    selector: 'so-task-info',
    templateUrl: './task-info.component.html',
    styleUrls: ['./task-info.component.sass']
})
export class TaskInfoComponent extends BaseComponent implements OnInit {
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

    public getBlockStatus(): string {
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

    public markAsCompleted(): void {
        this.store.dispatch(dashboardActions.markTaskCompleted());
    }

    public delete(): void {
        this.store.dispatch(dashboardActions.deleteTask());
    }

    public edit(): void {
        this.store.dispatch(dashboardActions.openSidebar({ sidebarType: SidebarType.Edit }));
    }
}

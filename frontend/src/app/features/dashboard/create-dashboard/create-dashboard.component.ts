import * as dashboardActions from '@store/actions/dashboard.actions';

import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { BaseComponent } from '@shared/components/base/base.component';
import { Store } from '@ngrx/store';
import { selectUserId } from '@store/selectors/auth-page.selectors';

@Component({
    selector: 'app-create-dashboard',
    templateUrl: './create-dashboard.component.html',
    styleUrls: ['./create-dashboard.component.sass']
})
export class CreateDashboardComponent extends BaseComponent {
    @Output() public closed = new EventEmitter();

    public dashboardTitleControl: FormControl = new FormControl('', [Validators.required]);

    constructor(private store: Store) {
        super();
    }

    public submit(): void {
        this.store.select(selectUserId)
            .pipe(this.untilDestroyed)
            .subscribe((id) => {
                this.store.dispatch(dashboardActions.createDashbaord( { dashboard: {
                    userId: id,
                    title: this.dashboardTitleControl.value
                }}));
                this.closed.emit();
            });
    }

    public cancel(): void {
        this.closed.emit();
    }
}

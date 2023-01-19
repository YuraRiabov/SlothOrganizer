import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { Dashboard } from '#types/dashboard/dashboard/dashboard';

@Component({
    selector: 'so-dashboard-selection',
    templateUrl: './dashboard-selection.component.html',
    styleUrls: ['./dashboard-selection.component.sass']
})
export class DashboardSelectionComponent {
    @Output() private newDashboardTitle = new EventEmitter<string>();
    @Output() public selectDashboard = new EventEmitter<Dashboard>();

    @Input() public dashboards: Dashboard[] = [];
    @Input() public selectedDashboard: Dashboard | null = null;

    public creating: boolean = false;
    public dashboardTitleControl: FormControl = new FormControl('', [Validators.required]);

    public createDashboard(): void {
        this.newDashboardTitle.emit(this.dashboardTitleControl.value);
        this.creating = false;
    }
}

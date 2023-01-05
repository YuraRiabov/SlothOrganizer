import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { endRepeatingValidator, getDescriptionValidators, getTitleValidators, repeatingPeriodValidator, startBeforeEndValidator } from '@utils/validators/task-validators';
import { getEnd, getPeriodLabel, getStart } from '@utils/form-helpers/task-form.helper';

import { BaseComponent } from '@shared/components/base/base.component';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { Store } from '@ngrx/store';
import { TaskRepeatingPeriod } from '#types/dashboard/tasks/enums/task-repeating-period';
import { createTask } from '@store/actions/dashboard.actions';
import { getRepeatingPeriods } from '@utils/creation-functions/repeating-period.helper';
import { hasLengthErrors } from '@utils/validators/common-validators';
import { selectChosenDashboardId } from '@store/selectors/dashboard.selectors';

@Component({
    selector: 'so-task-form',
    templateUrl: './task-form.component.html',
    styleUrls: ['./task-form.component.sass']
})
export class TaskFormComponent extends BaseComponent implements OnInit {
    @Output() private cancel = new EventEmitter<void>();
    public taskForm: FormGroup = {} as FormGroup;

    public isRepeating: boolean = false;
    public selectStartTime: boolean = false;
    public selectEndTime: boolean = false;

    public repeatingOptions: TaskRepeatingPeriod[] = getRepeatingPeriods();

    constructor(private store: Store) {
        super();
    }

    ngOnInit(): void {
        this.taskForm = this.buildTaskFrom();
    }

    public getLabel(period: TaskRepeatingPeriod): string {
        return getPeriodLabel(period);
    }

    public validate(): void {
        this.taskForm.get('startDate')?.updateValueAndValidity();
        this.taskForm.get('endDate')?.updateValueAndValidity();
        this.taskForm.get('endRepeating')?.updateValueAndValidity();
        this.update();
    }

    public update(): void {
        this.isRepeating = (this.taskForm.get('repeatingPeriod')?.value as TaskRepeatingPeriod) !== TaskRepeatingPeriod.None;
        this.selectStartTime = this.taskForm.get('startTimeCheckbox')?.value;
        this.selectEndTime = this.taskForm.get('endTimeCheckbox')?.value;
    }

    public hasLengthErrors(controlName: string): boolean {
        return hasLengthErrors(this.taskForm, controlName);
    }

    public cancelClick(): void {
        this.cancel.emit();
    }

    public saveClick(): void {
        this.store.select(selectChosenDashboardId).pipe(
            this.untilDestroyed
        ).subscribe(id => {
            const newTask: NewTask = {
                dashboardId: id,
                title: this.taskForm.get('title')?.value,
                description: this.taskForm.get('description')?.value,
                start: getStart(this.taskForm),
                end: getEnd(this.taskForm),
                repeatingPeriod: this.taskForm.get('repeatingPeriod')?.value,
                endRepeating: this.taskForm.get('endRepeating')?.value
            };
            this.store.dispatch(createTask({ newTask }));
            this.cancel.emit();
        });
    }

    private buildTaskFrom(): FormGroup {
        const titleContol = new FormControl('', getTitleValidators());
        const descriptionControl = new FormControl('', getDescriptionValidators());

        return new FormGroup({
            title: titleContol,
            description: descriptionControl,
            startDate: new FormControl('', Validators.required),
            endDate: new FormControl('', Validators.required),
            startTimeCheckbox: new FormControl(false),
            startTime: new FormControl('12:00'),
            endTimeCheckbox: new FormControl(false),
            endTime: new FormControl('12:00'),
            repeatingPeriod: new FormControl(TaskRepeatingPeriod.None),
            endRepeating: new FormControl()
        }, {
            validators: [
                startBeforeEndValidator(),
                repeatingPeriodValidator(),
                endRepeatingValidator()
            ]
        });
    }
}

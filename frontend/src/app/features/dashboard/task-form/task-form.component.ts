import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { addDays, endOfDay, startOfDay } from 'date-fns';
import { endRepeatingValidator, getDescriptionValidators, getTitleValidators, repeatingPeriodValidator, startBeforeEndValidator } from '@utils/validators/task-validators';
import { getEnd, getPeriodLabel, getStart } from '@utils/form-helpers/task-form.helper';

import { BaseComponent } from '@shared/components/base/base.component';
import { DatePipe } from '@angular/common';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { TaskRepeatingPeriod } from '#types/dashboard/tasks/enums/task-repeating-period';
import { getRepeatingPeriods } from '@utils/creation-functions/repeating-period.helper';
import { hasLengthErrors } from '@utils/validators/common-validators';

@Component({
    selector: 'so-task-form',
    templateUrl: './task-form.component.html',
    styleUrls: ['./task-form.component.sass']
})
export class TaskFormComponent extends BaseComponent {
    private dashboardId!: number;
    @Input() public set initialValue(value: NewTask) {
        this.isRepeating = !!value.endRepeating;
        this.dashboardId = value.dashboardId;
        this.taskForm = this.buildTaskFrom(value);
        this.update();
    }
    @Output() public submitted = new EventEmitter<NewTask>();
    @Input() public set edit(value: boolean) {
        this.editing = value;
        this.update();
    }
    @Output() public cancel = new EventEmitter<void>();
    public taskForm: FormGroup = {} as FormGroup;
    public title: string = '';

    public editing: boolean = false;
    public isRepeating: boolean = false;
    public selectStartTime: boolean = false;
    public selectEndTime: boolean = false;

    public repeatingOptions: TaskRepeatingPeriod[] = getRepeatingPeriods();

    constructor() {
        super();
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
        this.title = this.editing ? 'Edit task' : 'Create task';
        this.isRepeating = this.editing
            ? !!this.taskForm.get('endRepeating')?.value
            : (this.taskForm.get('repeatingPeriod')?.value as TaskRepeatingPeriod) !== TaskRepeatingPeriod.None;
        this.selectStartTime = this.taskForm.get('startTimeCheckbox')?.value;
        this.selectEndTime = this.taskForm.get('endTimeCheckbox')?.value;
    }

    public hasLengthErrors(controlName: string): boolean {
        return hasLengthErrors(this.taskForm, controlName);
    }

    public createTask(): void {
        const newTask: NewTask = {
            dashboardId: this.dashboardId,
            title: this.taskForm.get('title')?.value,
            description: this.taskForm.get('description')?.value,
            start: getStart(this.taskForm),
            end: getEnd(this.taskForm),
            repeatingPeriod: this.taskForm.get('repeatingPeriod')?.value,
            endRepeating: addDays(this.taskForm.get('endRepeating')?.value, 1)
        };
        this.submitted.emit(newTask);
    }

    private buildTaskFrom(initialValue: NewTask): FormGroup {
        const titleContol = new FormControl(initialValue.title, getTitleValidators());
        const descriptionControl = new FormControl(initialValue.description, getDescriptionValidators());

        const validators = [ startBeforeEndValidator(), endRepeatingValidator()];
        if (!this.editing) {
            validators.push(repeatingPeriodValidator());
        }
        const datePipe = new DatePipe('en-US');

        return new FormGroup({
            title: titleContol,
            description: descriptionControl,
            startDate: new FormControl(startOfDay(initialValue.start), Validators.required),
            endDate: new FormControl((endOfDay(initialValue.end)), Validators.required),
            startTimeCheckbox: new FormControl(initialValue.start.getTime() !== startOfDay(initialValue.start).getTime()),
            startTime: new FormControl(datePipe.transform(initialValue.start, 'HH:mm')),
            endTimeCheckbox: new FormControl(initialValue.end.getTime() !== endOfDay(initialValue.end).getTime()),
            endTime: new FormControl(datePipe.transform(initialValue.end, 'HH:mm')),
            repeatingPeriod: new FormControl(initialValue.repeatingPeriod),
            endRepeating: new FormControl(initialValue.endRepeating)
        }, {
            validators
        });
    }
}

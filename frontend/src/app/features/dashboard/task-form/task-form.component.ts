import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { endOfDay, startOfDay } from 'date-fns';
import { endRepeatingValidator, getDescriptionValidators, getTitleValidators, repeatingPeriodValidator, startBeforeEndValidator } from '@utils/validators/task-validators';
import { getEnd, getPeriodLabel, getStart } from '@utils/form-helpers/task-form.helper';

import { BaseComponent } from '@shared/components/base/base.component';
import { DatePipe } from '@angular/common';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { Store } from '@ngrx/store';
import { TaskRepeatingPeriod } from '#types/dashboard/tasks/enums/task-repeating-period';
import { getRepeatingPeriods } from '@utils/creation-functions/repeating-period.helper';
import { hasLengthErrors } from '@utils/validators/common-validators';

@Component({
    selector: 'so-task-form',
    templateUrl: './task-form.component.html',
    styleUrls: ['./task-form.component.sass']
})
export class TaskFormComponent extends BaseComponent implements OnInit {
    @Input() public initialValue!: NewTask;
    @Output() public submitted = new EventEmitter<NewTask>();
    @Input() public editing: boolean = false;
    @Output() public cancel = new EventEmitter<void>();
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

    public createTask(): void {
        const newTask: NewTask = {
            dashboardId: this.initialValue.dashboardId,
            title: this.taskForm.get('title')?.value,
            description: this.taskForm.get('description')?.value,
            start: getStart(this.taskForm),
            end: getEnd(this.taskForm),
            repeatingPeriod: this.taskForm.get('repeatingPeriod')?.value,
            endRepeating: this.taskForm.get('endRepeating')?.value
        };
        this.submitted.emit(newTask);
    }

    private buildTaskFrom(): FormGroup {
        const titleContol = new FormControl(this.initialValue.title, getTitleValidators());
        const descriptionControl = new FormControl(this.initialValue.description, getDescriptionValidators());

        let validators = [ startBeforeEndValidator(), endRepeatingValidator()];
        if (!this.editing) {
            validators.push(repeatingPeriodValidator());
        }
        let datePipe = new DatePipe('en-US');

        return new FormGroup({
            title: titleContol,
            description: descriptionControl,
            startDate: new FormControl(startOfDay(this.initialValue.start), Validators.required),
            endDate: new FormControl((endOfDay(this.initialValue.end)), Validators.required),
            startTimeCheckbox: new FormControl(this.initialValue.start.getTime() !== startOfDay(this.initialValue.start).getTime()),
            startTime: new FormControl(datePipe.transform(this.initialValue.start, 'HH:mm')),
            endTimeCheckbox: new FormControl(this.initialValue.end.getTime() !== endOfDay(this.initialValue.end).getTime()),
            endTime: new FormControl(datePipe.transform(this.initialValue.end, 'HH:mm')),
            repeatingPeriod: new FormControl(this.initialValue.repeatingPeriod),
            endRepeating: new FormControl(this.initialValue.endRepeating)
        }, {
            validators
        });
    }
}

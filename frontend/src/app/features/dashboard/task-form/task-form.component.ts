import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { addDays, differenceInDays, endOfDay, startOfDay } from 'date-fns';

import { BaseComponent } from '@shared/components/base/base.component';
import { DatePipe } from '@angular/common';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { TaskRepeatingPeriod } from '#types/dashboard/tasks/enums/task-repeating-period';
import { getRepeatingPeriods } from '@utils/creation-functions/repeating-period.helper';
import { hasLengthErrors } from '@utils/validators/common-validators';
import { setTime } from '@utils/dates/dates.helper';

@Component({
    selector: 'so-task-form',
    templateUrl: './task-form.component.html',
    styleUrls: ['./task-form.component.sass']
})
export class TaskFormComponent extends BaseComponent {
    @Input() public set initialValue(value: NewTask | null) {
        const initialValue = value ?? {
            dashboardId: -1,
            title: '',
            description: '',
            repeatingPeriod: TaskRepeatingPeriod.None,
            start: startOfDay(new Date()),
            end: endOfDay(new Date()),
        };
        this.isRepeating = !!initialValue.endRepeating;
        this.taskForm = this.buildTaskFrom(initialValue);
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
        switch (period) {
        case TaskRepeatingPeriod.None:
            return 'None';
        case TaskRepeatingPeriod.Day:
            return 'Day';
        case TaskRepeatingPeriod.Week:
            return 'Week';
        case TaskRepeatingPeriod.Month:
            return 'Month';
        case TaskRepeatingPeriod.Year:
            return 'Year';
        default:
            throw new Error('Invalid repeating period');
        }
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
            title: this.taskForm.get('title')?.value,
            description: this.taskForm.get('description')?.value,
            start: this.getStart(this.taskForm),
            end: this.getEnd(this.taskForm),
            repeatingPeriod: this.taskForm.get('repeatingPeriod')?.value,
            endRepeating: addDays(this.taskForm.get('endRepeating')?.value, 1)
        };
        this.submitted.emit(newTask);
    }

    private buildTaskFrom(initialValue: NewTask): FormGroup {
        const titleContol = new FormControl(initialValue.title, this.getTitleValidators());
        const descriptionControl = new FormControl(initialValue.description, this.getDescriptionValidators());

        const validators = [this.getStartBeforeEndValidator(), this.getEndRepeatingValidator()];
        if (!this.editing) {
            validators.push(this.getRepeatingPeriodValidator());
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

    private getTitleValidators(): Validators {
        return [
            Validators.required,
            Validators.minLength(2),
            Validators.maxLength(60)
        ];
    }

    private getDescriptionValidators(): Validators {
        return [Validators.maxLength(400)];
    }

    private getStartBeforeEndValidator(): ValidatorFn {
        return (group: AbstractControl) => {
            const startDateControl = group.get('startDate');
            const endDateControl = group.get('endDate');
            const start = this.getStart(group);
            const end = this.getEnd(group);
            if (start > end) {
                startDateControl?.setErrors({ startBeforeEnd: true });
                endDateControl?.setErrors({ startBeforeEnd: true });
            }
            return null;
        };
    }

    private getRepeatingPeriodValidator(): ValidatorFn {
        return (group: AbstractControl) => {
            const repeatingPeriodControl = group.get('repeatingPeriod');
            const end = this.getEnd(group);
            const start = this.getStart(group);
            const repeatingPeriod = repeatingPeriodControl?.value as TaskRepeatingPeriod;
            if (this.getPeriodDays(repeatingPeriod) < differenceInDays(end, start)) {
                repeatingPeriodControl?.setErrors({ shortRepeatingPeriod: true });
            }
            return null;
        };
    }

    private getEndRepeatingValidator(): ValidatorFn {
        return (group: AbstractControl) => {
            const repeatingPeriodControl = group.get('repeatingPeriod');
            const endRepeatingControl = group.get('endRepeating');
            const end = this.getEnd(group);
            const repeatingPeriod = repeatingPeriodControl?.value as TaskRepeatingPeriod;
            const endRepeating = endRepeatingControl?.value as Date;
            if (repeatingPeriod !== TaskRepeatingPeriod.None && end > endRepeating) {
                endRepeatingControl?.setErrors({ endBeforeEndRepeating: true });
            }
            return null;
        };
    }

    private getStart(group: AbstractControl): Date {
        const startDateControl = group.get('startDate');
        const startTimeControl = group.get('startTime');
        const startTimeCheckbox = group.get('startTimeCheckbox');

        let startDate = new Date(startDateControl?.value);
        if (startTimeCheckbox?.value) {
            startDate = setTime(startDate, startTimeControl?.value);
        } else {
            startDate = startOfDay(startDate);
        }
        return startDate;
    }

    private getEnd(group: AbstractControl): Date {
        const endDateControl = group.get('endDate');
        const endTimeControl = group.get('endTime');
        const endTimeCheckbox = group.get('endTimeCheckbox');

        let endDate = new Date(endDateControl?.value);
        if (endTimeCheckbox?.value) {
            endDate = setTime(endDate, endTimeControl?.value);
        } else {
            endDate = endOfDay(endDate);
        }
        return endDate;
    }

    private getPeriodDays(period: TaskRepeatingPeriod) {
        switch (period) {
        case TaskRepeatingPeriod.None:
            return Number.MAX_SAFE_INTEGER;
        case TaskRepeatingPeriod.Day:
            return 1;
        case TaskRepeatingPeriod.Week:
            return 7;
        case TaskRepeatingPeriod.Month:
            return 28;
        case TaskRepeatingPeriod.Year:
            return 365;
        default:
            throw new Error('Invalid repeating period');
        }
    }
}

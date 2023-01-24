import * as dashboardActions from '@store/actions/dashboard.actions';

import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { differenceInDays, endOfDay, startOfDay } from 'date-fns';

import { BaseComponent } from '@shared/components/base/base.component';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { Store } from '@ngrx/store';
import { TaskRepeatingPeriod } from '#types/dashboard/tasks/enums/task-repeating-period';
import { getRepeatingPeriods } from '@utils/creation-functions/repeating-period.helper';
import { hasLengthErrors } from '@utils/validators/common-validators';
import { selectChosenDashboardId } from '@store/selectors/dashboard.selectors';
import { setTime } from '@utils/dates/dates.helper';
import { take } from 'rxjs';

@Component({
    selector: 'so-task-form',
    templateUrl: './task-form.component.html',
    styleUrls: ['./task-form.component.sass']
})
export class TaskFormComponent extends BaseComponent implements OnInit {
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
        this.isRepeating = (this.taskForm.get('repeatingPeriod')?.value as TaskRepeatingPeriod) !== TaskRepeatingPeriod.None;
        this.selectStartTime = this.taskForm.get('startTimeCheckbox')?.value;
        this.selectEndTime = this.taskForm.get('endTimeCheckbox')?.value;
    }

    public hasLengthErrors(controlName: string): boolean {
        return hasLengthErrors(this.taskForm, controlName);
    }

    public createTask(): void {
        this.store.select(selectChosenDashboardId).pipe(
            this.untilDestroyed,
            take(1)
        ).subscribe(id => {
            const newTask: NewTask = {
                dashboardId: id,
                title: this.taskForm.get('title')?.value,
                description: this.taskForm.get('description')?.value,
                start: this.getStart(this.taskForm),
                end: this.getEnd(this.taskForm),
                repeatingPeriod: this.taskForm.get('repeatingPeriod')?.value,
                endRepeating: this.taskForm.get('endRepeating')?.value
            };
            this.store.dispatch(dashboardActions.createTask({ newTask }));
            this.closeForm();
        });
    }

    public closeForm(): void {
        this.store.dispatch(dashboardActions.closeSidebar());
    }

    private buildTaskFrom(): FormGroup {
        const titleContol = new FormControl('', this.getTitleValidators());
        const descriptionControl = new FormControl('', this.getDescriptionValidators());

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
                this.getStartBeforeEndValidator(),
                this.getRepeatingPeriodValidator(),
                this.getEndRepeatingValidator()
            ]
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

    private getEndRepeatingValidator = (): ValidatorFn => {
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
    };

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

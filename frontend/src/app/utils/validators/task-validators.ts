import { AbstractControl, ValidatorFn, Validators } from '@angular/forms';
import { getEnd, getPeriodDays, getStart } from '@utils/form-helpers/task-form.helper';

import { TaskRepeatingPeriod } from '#types/dashboard/tasks/enums/task-repeating-period';
import { differenceInDays } from 'date-fns';

export const getTitleValidators = (): Validators => [
    Validators.required,
    Validators.minLength(2),
    Validators.maxLength(60)
];

export const getDescriptionValidators = (): Validators => [
    Validators.maxLength(400)
];

export const startBeforeEndValidator = () : ValidatorFn => {
    return (group: AbstractControl) => {
        const startDateControl = group.get('startDate');
        const endDateControl = group.get('endDate');
        const start = getStart(group);
        const end = getEnd(group);
        if (start > end) {
            startDateControl?.setErrors({ startBeforeEnd: true });
            endDateControl?.setErrors({ startBeforeEnd: true });
        }
        return null;
    };
};

export const repeatingPeriodValidator = (): ValidatorFn => {
    return (group: AbstractControl) => {
        const repeatingPeriodControl = group.get('repeatingPeriod');
        const end = getEnd(group);
        const start = getStart(group);
        const repeatingPeriod = repeatingPeriodControl?.value as TaskRepeatingPeriod;
        if (getPeriodDays(repeatingPeriod) < differenceInDays(end, start)) {
            repeatingPeriodControl?.setErrors({ shortRepeatingPeriod: true });
        }
        return null;
    };
};

export const endRepeatingValidator = () : ValidatorFn => {
    return (group: AbstractControl) => {
        const repeatingPeriodControl = group.get('repeatingPeriod');
        const endRepeatingControl = group.get('endRepeating');
        const end = getEnd(group);
        const repeatingPeriod = repeatingPeriodControl?.value as TaskRepeatingPeriod;
        const endRepeating = endRepeatingControl?.value as Date;
        if (repeatingPeriod !== TaskRepeatingPeriod.None && end > endRepeating) {
            endRepeatingControl?.setErrors({ endBeforeEndRepeating: true });
        }
        return null;
    };
};
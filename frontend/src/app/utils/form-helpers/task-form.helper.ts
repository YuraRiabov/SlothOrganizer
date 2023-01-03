import { endOfDay, startOfDay } from 'date-fns';

import { AbstractControl } from '@angular/forms';
import { TaskRepeatingPeriod } from '#types/dashboard/tasks/enums/task-repeating-period';
import { setTime } from '@utils/dates/dates.helper';

export const getStart = (group: AbstractControl) => {
    const startDateControl = group.get('startDate');
    const startTimeControl = group.get('startTime');
    const startTimeCheckbox = group.get('startTimeCheckbox');

    let startDate = startDateControl?.value as Date;
    if (startTimeCheckbox?.value) {
        startDate = setTime(startDate, startTimeControl?.value);
    } else {
        startDate = startOfDay(startDate);
    }
    return startDate;
};

export const getEnd = (group: AbstractControl) => {
    const endDateControl = group.get('endDate');
    const endTimeControl = group.get('endTime');
    const endTimeCheckbox = group.get('endTimeCheckbox');

    let endDate = endDateControl?.value as Date;
    if (endTimeCheckbox?.value) {
        endDate = setTime(endDate, endTimeControl?.value);
    } else {
        endDate = endOfDay(endDate);
    }
    return endDate;
};

export const getPeriodDays = (period: TaskRepeatingPeriod) => {
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
};

export const getPeriodLabel = (period: TaskRepeatingPeriod) => {
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
};
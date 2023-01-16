import { addMinutes, setHours, setMinutes } from 'date-fns';

export const intercept = (first: { start: Date, end: Date }, second: { start: Date, end: Date }) => {
    return first.end >= second.start && second.end >= first.start;
};

export const setTime = (date: Date, time: string) => {
    const splitTime = time.split(':').map(x => Number.parseInt(x));
    return setHours(setMinutes(date, splitTime[1]), splitTime[0]);
};

export const toLocal = (date: Date) => {
    return addMinutes(date, -1 * new Date().getTimezoneOffset());
};

export const hasTimezone = (date: string) => {
    return date.includes('+') || date.includes('Z');
};

export const HOURS_IN_DAY = 24;

export const MAX_DAYS_IN_MONTH = 31;
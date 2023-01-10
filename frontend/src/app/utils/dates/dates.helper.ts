import { addMinutes, setHours, setMinutes } from 'date-fns';

export const isBetween = (date: Date, start: Date, end: Date) => {
    return start <= date && date <= end;
};

export const setTime = (date: Date, time: string) => {
    let splitTime = time.split(':').map(x => Number.parseInt(x));
    return setHours(setMinutes(date, splitTime[1]), splitTime[0]);
};

export const toLocal = (date: Date) => {
    return addMinutes(date, -1 * new Date().getTimezoneOffset());
};

export const hasTimezone = (date: string) => {
    return date.includes('+') || date.includes('Z');
};
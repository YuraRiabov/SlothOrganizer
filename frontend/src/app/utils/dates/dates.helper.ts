import { addMinutes, setHours, setMinutes, setSeconds } from 'date-fns';

export const isBetween = (date: Date, start: Date, end: Date) => {
    return start <= date && date <= end;
};

export const setTime = (date: Date, time: string) => {
    let splitTime = time.split(':').map(x => Number.parseInt(x));
    return new Date(date.getFullYear(), date.getMonth(), date.getDate(), splitTime[0], splitTime[1]);
};

export const toLocal = (date: Date) => {
    return addMinutes(date, -1 * new Date().getTimezoneOffset());
};

export const hasTimezone = (date: string) => {
    return date.includes('+') || date.includes('Z');
};
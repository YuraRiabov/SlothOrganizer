export const intercept = (first: { start: Date, end: Date }, second: { start: Date, end: Date }) => {
    return first.end >= second.start && second.end >= first.start;
};

export const HOURS_IN_DAY = 24;

export const MAX_DAYS_IN_MONTH = 31;
export const intercept = (first: { start: Date, end: Date }, second: { start: Date, end: Date }) => {
    return first.end >= second.start && second.end >= first.start;
};

export const hoursInDay = 24;

export const maxDaysInMonth = 31;
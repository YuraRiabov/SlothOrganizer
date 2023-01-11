export const isBetween = (date: Date, start: Date, end: Date) => {
    return start <= date && date <= end;
};

export const hoursInDay = 24;

export const maxDaysInMonth = 31;
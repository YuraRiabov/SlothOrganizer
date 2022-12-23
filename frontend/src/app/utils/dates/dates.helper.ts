export const isBetween = (date: Date, start: Date, end: Date) => {
    return start <= date && date <= end;
};
export const isBetween = (date: Date, start: Date, end: Date) => {
    return start <= date && date <= end;
};

export const getDayStart = (date: Date) => {
    return new Date(date.getFullYear(), date.getMonth(), date.getDate());
};

export const getWeekStart = (date: Date) => {
    const day = date.getDate() - date.getDay() + 1;
    return new Date(date.getFullYear(), date.getMonth(), day);
};

export const getMonthStart = (date: Date) => {
    return new Date(date.getFullYear(), date.getMonth(), 1);
};

export const getYearStart = (date: Date) => {
    return new Date(date.getFullYear(), 0, 1);
};
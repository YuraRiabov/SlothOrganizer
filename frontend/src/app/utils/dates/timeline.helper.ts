import { addDays, addHours, addMonths, addWeeks, addYears, differenceInHours } from 'date-fns';
import { getDayStart, getMonthStart, getWeekStart, getYearStart } from './dates.helper';

import { TimelineBoundaries } from '#types/tasks/timeline-boundaries';
import { TimelineScale } from '#types/tasks/enums/timeline-scale';

export const getTimelineBoundaries = (currentDate: Date, scale: TimelineScale): TimelineBoundaries => {
    let start: Date;
    if (scale === TimelineScale.Day) {
        start = getDayStart(currentDate);
        return {
            start,
            end: addDays(start, 1)
        };
    }
    if (scale === TimelineScale.Week) {
        start = getWeekStart(currentDate);
        return {
            start,
            end: addWeeks(start, 1)
        };
    }
    if (scale === TimelineScale.Month) {
        start = getMonthStart(currentDate);
        return {
            start,
            end: addMonths(start, 1)
        };
    }
    if (scale === TimelineScale.Year) {
        start = getYearStart(currentDate);
        return {
            start,
            end: addYears(start, 1)
        };
    }
    throw new Error('Invalid scale');
};

export const getTimelineSubsections = (boundaries: TimelineBoundaries, scale: TimelineScale): Date[] => {
    let sectionSize: number = getSubsectionSize(scale, boundaries);
    const sectionsCount : number = differenceInHours(boundaries.start, boundaries.end) / sectionSize;
    let sections: Date[] = [];
    for (let i = 0; i < sectionsCount; i++) {
        sections.push(addHours(boundaries.start, sectionSize * (i + 1)));
    }
    return sections;
};

const getSubsectionSize = (scale: TimelineScale, boundaries: TimelineBoundaries) => {
    let sectionSize: number;
    switch (scale) {
    case TimelineScale.Day:
        sectionSize = differenceInHours(boundaries.start, addHours(boundaries.start, 4));
        break;
    case TimelineScale.Week:
        sectionSize = differenceInHours(boundaries.start, addDays(boundaries.start, 1));
        break;
    case TimelineScale.Month:
        sectionSize = differenceInHours(boundaries.start, addWeeks(boundaries.start, 1));
        break;
    case TimelineScale.Year:
        sectionSize = differenceInHours(boundaries.start, addMonths(boundaries.start, 1));
        break;
    }
    return sectionSize;
};

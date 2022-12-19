import { addDays, addHours, addMonths, addWeeks, addYears, differenceInHours } from 'date-fns';
import { getDayStart, getMonthStart, getWeekStart, getYearStart } from './dates.helper';

import { DatePipe } from '@angular/common';
import { TimelineBoundaries } from '#types/tasks/timeline-boundaries';
import { TimelineScale } from '#types/tasks/enums/timeline-scale';

export const getTimelineBoundaries = (currentDate: Date, scale: TimelineScale): TimelineBoundaries => {
    let start: Date;
    switch (scale) {
    case TimelineScale.Day:
        start = getDayStart(currentDate);
        return {
            start,
            end: addDays(start, 1)
        };
    case TimelineScale.Week:
        start = getWeekStart(currentDate);
        return {
            start,
            end: addWeeks(start, 1)
        };
    case TimelineScale.Month:
        start = getMonthStart(currentDate);
        return {
            start,
            end: addMonths(start, 1)
        };
    case TimelineScale.Year:
        start = getYearStart(currentDate);
        return {
            start,
            end: addYears(start, 1)
        };
    default:
        throw new Error('Invalid scale');
    }
};

export const getTimelineSubsections = (boundaries: TimelineBoundaries, scale: TimelineScale): Date[] => {
    let sectionSize: number = getSubsectionSize(scale);
    const sectionsCount : number = differenceInHours(boundaries.end, boundaries.start) / sectionSize;
    let sections: Date[] = [];
    for (let i = 0; i < sectionsCount; i++) {
        sections.push(addHours(boundaries.start, sectionSize * (i + 1)));
    }
    return sections;
};

export const getSubsectionTitle = (subsection: Date, scale: TimelineScale): string => {
    let datePipe = new DatePipe('en-US');
    let sectionHours = getSubsectionSize(scale);
    switch (scale) {
    case TimelineScale.Day:
        return `${datePipe.transform(addHours(subsection, -1 * sectionHours), 'HH:mm')} - ${datePipe.transform(subsection, 'HH:mm')}`;
    case TimelineScale.Week:
        return datePipe.transform(addHours(subsection, -1), 'EEEE')!;
    case TimelineScale.Month:
        return `${datePipe.transform(addDays(addHours(subsection, -1 * sectionHours), 1), 'MM/dd')} - ${datePipe.transform(subsection, 'MM/dd')}`;
    case TimelineScale.Year:
        return datePipe.transform(addHours(subsection, -1), 'MMMM')!;
    default:
        throw new Error('Invalid scale');
    }
};

const getSubsectionSize = (scale: TimelineScale) => {
    let mockDate: Date = new Date();
    switch (scale) {
    case TimelineScale.Day:
        return differenceInHours(addHours(mockDate, 4), mockDate);
    case TimelineScale.Week:
        return differenceInHours(addDays(mockDate, 1), mockDate);
    case TimelineScale.Month:
        return differenceInHours(addWeeks(mockDate, 1), mockDate);
    case TimelineScale.Year:
        return differenceInHours(addMonths(mockDate, 1), mockDate);
    default:
        throw new Error('Invalid scale');
    }
};

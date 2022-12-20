import { addDays, addHours, addMonths, addWeeks, addYears, differenceInHours, endOfMonth, startOfMonth, sub } from 'date-fns';
import { getDayStart, getMonthStart, getWeekStart, getYearStart } from './dates.helper';

import { DatePipe } from '@angular/common';
import { TimelineScale } from '#types/tasks/enums/timeline-scale';
import { TimelineSection } from '#types/tasks/timeline-section';

export const getTimelineBoundaries = (currentDate: Date, scale: TimelineScale): TimelineSection => {
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

export const getTimelineSubsections = (boundaries: TimelineSection, scale: TimelineScale): TimelineSection[] => {
    let sectionSize: number = getSubsectionSize(scale);
    const sectionsCount : number = differenceInHours(boundaries.end, boundaries.start) / sectionSize;
    let sections: TimelineSection[] = [];
    for (let i = 0; i < sectionsCount; i++) {
        let section: TimelineSection;
        if (scale === TimelineScale.Year) {
            section = {
                start: startOfMonth(addHours(boundaries.start, sectionSize * i)),
                end: endOfMonth(addHours(boundaries.start, sectionSize * i)),
            };
        } else {
            section = {
                start: addHours(boundaries.start, sectionSize * i),
                end: addHours(boundaries.start, sectionSize * (i + 1))
            };
        }
        sections.push(section);
    }
    return sections;
};

export const getSubsectionTitle = (subsection: TimelineSection, scale: TimelineScale): string => {
    let datePipe = new DatePipe('en-US');
    switch (scale) {
    case TimelineScale.Day:
        return `${datePipe.transform(subsection.start, 'HH:mm')} - ${datePipe.transform(subsection.end, 'HH:mm')}`;
    case TimelineScale.Week:
        return datePipe.transform(subsection.start, 'EEEE')!;
    case TimelineScale.Month:
        return `${datePipe.transform(subsection.start, 'MM/dd')} - ${datePipe.transform(subsection.end, 'MM/dd')}`;
    case TimelineScale.Year:
        return datePipe.transform(subsection.start, 'MMMM')!;
    default:
        throw new Error('Invalid scale');
    }
};

const getSubsectionSize = (scale: TimelineScale) => {
    let mockDate: Date = new Date();
    switch (scale) {
    case TimelineScale.Day:
        return 4;
    case TimelineScale.Week:
        return 24;
    case TimelineScale.Month:
        return 24 * 7;
    case TimelineScale.Year:
        return 24 * 32;
    default:
        throw new Error('Invalid scale');
    }
};

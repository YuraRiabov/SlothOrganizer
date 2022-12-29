import { addDays, addHours, addMonths, addWeeks, addYears, daysInWeek, differenceInHours, differenceInYears, endOfDay, endOfMonth, endOfWeek, endOfYear, startOfDay, startOfMonth, startOfWeek, startOfYear } from 'date-fns';

import { DatePipe } from '@angular/common';
import { TimelineScale } from '#types/dashboard/timeline/enums/timeline-scale';
import { TimelineSection } from '#types/dashboard/timeline/timeline-section';

export const getTimelineBoundaries = (currentDate: Date, scale: TimelineScale, pageNumber: number): TimelineSection => {
    switch (scale) {
    case TimelineScale.Day:
        return {
            start: startOfDay(addDays(currentDate, -1 * pageNumber / 3)),
            end: endOfDay(addDays(currentDate, pageNumber / 3))
        };
    case TimelineScale.Week:
        return {
            start: startOfWeek(addWeeks(currentDate, -1 * pageNumber / 2)),
            end: endOfWeek(addWeeks(currentDate, pageNumber / 2))
        };
    case TimelineScale.Month:
        return {
            start: startOfMonth(addMonths(currentDate, -1 * pageNumber / 2)),
            end: endOfMonth(addMonths(currentDate, pageNumber / 2))
        };
    case TimelineScale.Year:
        return {
            start: startOfMonth(addYears(currentDate, -1 * pageNumber / 2)),
            end: endOfMonth(addYears(currentDate, pageNumber / 2))
        };
    default:
        throw new Error('Invalid scale');
    }
};

export const getTimelineSections = (boundaries: TimelineSection, scale: TimelineScale) => {
    if (scale !== TimelineScale.Year) {
        return getTimelineSubsections(boundaries, scale + 1);
    }
    let sectionHours = 24 * 367;
    let sectionsCount = differenceInYears(boundaries.end, boundaries.start);
    let sections: TimelineSection[] = [];
    let firstSectionStart = startOfYear(boundaries.start);
    sections.push({ start: firstSectionStart, end: endOfYear(firstSectionStart) });
    for (let i = 1; i < sectionsCount; i++) {
        let previousDate = sections[i - 1].start;
        let section = {
            start: startOfYear(addHours(previousDate, sectionHours)),
            end: endOfYear(addHours(previousDate, sectionHours)),
        };
        sections.push(section);
    }
    return sections;
};

export const getTimelineSubsections = (boundaries: TimelineSection, scale: TimelineScale): TimelineSection[] => {
    const sectionHours: number = getSubsectionSize(scale);
    let sectionsCount: number = Math.round(differenceInHours(boundaries.end, boundaries.start) / sectionHours);
    if (scale === TimelineScale.Month) {
        sectionsCount++;
    }
    if (scale == TimelineScale.Year) {
        sectionsCount = differenceInYears(boundaries.end, boundaries.start) * 12;
    }
    let sections: TimelineSection[] = [];
    const startFunction = getStartFunction(scale);
    const endFunction = getEndFunction(scale);

    const firstSectionStart = startFunction(boundaries.start);
    sections.push({ start: firstSectionStart, end: endFunction(firstSectionStart) });
    for (let i = 1; i < sectionsCount; i++) {
        let previousDate = sections[i - 1].start;
        let section = {
            start: startFunction(addHours(previousDate, sectionHours)),
            end: endFunction(addHours(previousDate, sectionHours)),
        };
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

export const getSectionTitle = (section: TimelineSection, scale: TimelineScale): string => {
    let datePipe = new DatePipe('en-US');
    switch (scale) {
    case TimelineScale.Day:
        return datePipe.transform(section.start, 'MM/dd/YYYY')!;
    case TimelineScale.Week:
        return `${datePipe.transform(section.start, 'MM/dd/YYYY')} - ${datePipe.transform(section.end, 'MM/dd/YYYY')}`;
    case TimelineScale.Month:
        return datePipe.transform(section.start, 'MMMM, YYYY')!;
    case TimelineScale.Year:
        return section.start.getFullYear().toString();
    default:
        throw new Error('Invalid scale');
    }
};

const getSubsectionSize = (scale: TimelineScale) => {
    switch (scale) {
    case TimelineScale.Day:
        return 4;
    case TimelineScale.Week:
        return 24;
    case TimelineScale.Month:
        return 24 * daysInWeek + 1;
    case TimelineScale.Year:
        return 24 * 32;
    default:
        throw new Error('Invalid scale');
    }
};

const getStartFunction = (scale: TimelineScale) => {
    switch (scale) {
    case TimelineScale.Day:
        return (date: Date) => date;
    case TimelineScale.Week:
        return startOfDay;
    case TimelineScale.Month:
        return startOfWeek;
    case TimelineScale.Year:
        return startOfMonth;
    default:
        throw new Error('Invalid scale');
    }
};

const getEndFunction = (scale: TimelineScale) => {
    switch (scale) {
    case TimelineScale.Day:
        return (date: Date) => addHours(date, 4);
    case TimelineScale.Week:
        return endOfDay;
    case TimelineScale.Month:
        return endOfWeek;
    case TimelineScale.Year:
        return endOfMonth;
    default:
        throw new Error('Invalid scale');
    }
};

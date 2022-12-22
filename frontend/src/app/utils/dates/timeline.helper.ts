import { addDays, addHours, addMonths, addWeeks, addYears, differenceInDays, differenceInHours, endOfDay, endOfMonth, endOfWeek, endOfYear, startOfDay, startOfMonth, startOfWeek, startOfYear } from 'date-fns';

import { DatePipe } from '@angular/common';
import { TimelineScale } from '#types/tasks/enums/timeline-scale';
import { TimelineSection } from '#types/tasks/timeline-section';

export const getTimelineBoundaries = (currentDate: Date, scale: TimelineScale, pageNumber: number): TimelineSection => {
    switch (scale) {
    case TimelineScale.Day:
        return {
            start: startOfDay(addDays(currentDate, -1 * pageNumber / 4)),
            end: endOfDay(addDays(currentDate, pageNumber / 4))
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
            start: startOfMonth(addYears(currentDate, -1 * pageNumber / 8)),
            end: endOfMonth(addYears(currentDate, pageNumber / 8))
        };
    default:
        throw new Error('Invalid scale');
    }
};

export const getTimelineSections = (boundaries: TimelineSection, scale: TimelineScale) => {
    if (scale !== TimelineScale.Year) {
        return getTimelineSubsections(boundaries, scale + 1);
    }
    let sectionHours = 24 * 365;
    let sectionsCount = differenceInHours(boundaries.end, boundaries.start) / sectionHours;
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
    let sectionHours: number = getSubsectionSize(scale);
    let sectionsCount: number = Math.round(differenceInHours(boundaries.end, boundaries.start) / sectionHours);
    let sections: TimelineSection[] = [];
    if (scale === TimelineScale.Year) {
        sectionsCount++;
        let firstSectionStart = startOfMonth(boundaries.start);
        sections.push({ start: firstSectionStart, end: endOfMonth(firstSectionStart) });
        for (let i = 1; i < sectionsCount; i++) {
            let previousDate = sections[i - 1].start;
            let section = {
                start: startOfMonth(addHours(previousDate, sectionHours)),
                end: endOfMonth(addHours(previousDate, sectionHours)),
            };
            sections.push(section);
        }
        return sections;
    }
    if (scale === TimelineScale.Month) {
        sectionsCount++;
        let firstSectionStart = startOfWeek(boundaries.start);
        sections.push({ start: firstSectionStart, end: endOfWeek(firstSectionStart) });
        for (let i = 1; i < sectionsCount; i++) {
            let previousDate = sections[i - 1].start;
            let section = {
                start: startOfWeek(addHours(previousDate, sectionHours)),
                end: endOfWeek(addHours(previousDate, sectionHours)),
            };
            sections.push(section);
        }
        return sections;
    }
    for (let i = 0; i < sectionsCount; i++) {
        let section: TimelineSection = {
            start: addHours(boundaries.start, sectionHours * i),
            end: addHours(boundaries.start, sectionHours * (i + 1))
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
        return datePipe.transform(section.start, 'YYYY')!;
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
        return 24 * 7 + 1;
    case TimelineScale.Year:
        return 24 * 32;
    default:
        throw new Error('Invalid scale');
    }
};

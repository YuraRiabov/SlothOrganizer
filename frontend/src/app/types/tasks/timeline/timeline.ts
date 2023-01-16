import { Task } from '../task';
import { TasksBlock } from './tasks-block';
import { TimelineSection } from './timeline-section';

export interface Timeline {
    columnNumber: number;
    pageNumber: number;
    tasksByRows: Task[][];
    exceedingTaskBlocks: TasksBlock[];
    boundaries: TimelineSection;
    sections: TimelineSection[];
    subsections: TimelineSection[];
}
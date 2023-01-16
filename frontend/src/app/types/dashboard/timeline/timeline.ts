import { JoinedTasksBlock } from './joined-tasks-block';
import { TaskBlock } from './task-block';
import { TimelineSection } from './timeline-section';

export interface Timeline {
    columnNumber: number;
    pageNumber: number;
    tasksByRows: TaskBlock[][];
    exceedingTaskBlocks: JoinedTasksBlock[];
    boundaries: TimelineSection;
    sections: TimelineSection[];
    subsections: TimelineSection[];
}
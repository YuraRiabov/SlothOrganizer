import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { addHours, differenceInHours } from 'date-fns';
import { getSectionTitle, getSubsectionTitle, getTimelineBoundaries, getTimelineSections, getTimelineSubsections } from '@utils/dates/timeline.helper';

import { Task } from '#types/tasks/task';
import { TasksBlock } from '#types/tasks/timeline/tasks-block';
import { TimelineScale } from '#types/tasks/timeline/enums/timeline-scale';
import { TimelineSection } from '#types/tasks/timeline/timeline-section';
import { isBetween } from '@utils/dates/dates.helper';

@Component({
    selector: 'so-timeline',
    templateUrl: './timeline.component.html',
    styleUrls: ['./timeline.component.sass']
})
export class TimelineComponent implements OnInit, AfterViewInit, OnDestroy {
    private readonly minimumColumnNumber = 15;
    private readonly taskRowsNumber = 5;
    public readonly pageColumnNumber = 500;
    public readonly defaultPageNumber = 12;

    private timelineScale: TimelineScale = TimelineScale.Day;
    private currentDate: Date = new Date();
    private returnDate: Date = this.currentDate;
    private timelineResizeObserver!: ResizeObserver;

    @Input() set date(value: Date) {
        this.currentDate = value;
        this.initializeTimeline();
    }
    @Input() public tasks: Task[] = [];
    @Input() public set scale(value: TimelineScale) {
        this.timelineScale = value;
        this.initializeTimeline();
    }

    @Output() scaleIncreased = new EventEmitter<Date>();

    @ViewChild('timelineScroll') timelineScroll!: ElementRef;
    @ViewChild('timelineContainer') timelineContainer!: ElementRef;

    public pageNumber: number = this.defaultPageNumber;
    public tasksByRows: Task[][] = [];
    public exceedingTaskBlocks: TasksBlock[] = [];
    public timelineBoundaries: TimelineSection = {} as TimelineSection;
    public timelineSections: TimelineSection[] = [];
    public timelineSubsections: TimelineSection[] = [];

    constructor() { }

    ngOnDestroy(): void {
        this.timelineResizeObserver.disconnect();
    }

    ngAfterViewInit(): void {
        this.scrollTo(this.currentDate);
        this.timelineResizeObserver = this.initializeTimelineWidthObserver();
    }

    ngOnInit(): void {
        this.initializeTimeline();
    }

    public getColumn(date: Date): number {
        if (date < this.timelineBoundaries.start) {
            return 1;
        }
        if (date > this.timelineBoundaries.end) {
            return this.pageColumnNumber * this.pageNumber + 1;
        }
        return Math.round(this.getDateRatio(date) * this.pageColumnNumber * this.pageNumber) + 1;
    }

    public getVisibleTasks(): Task[] {
        return this.tasks.filter(t => this.isVisible(t));
    }

    public getSubsectionTitle(subsection: TimelineSection): string {
        return getSubsectionTitle(subsection, this.timelineScale);
    }

    public getSectionTitle(section: TimelineSection): string {
        return getSectionTitle(section, this.timelineScale);
    }

    public increaseScale(section: TimelineSection): void {
        if (this.timelineScale !== TimelineScale.Day) {
            let date = addHours(section.start, differenceInHours(section.end, section.start) / 2);
            this.scaleIncreased.emit(date);
        }
    }

    public loadMore(left: boolean): void {
        this.returnDate = left ? this.timelineBoundaries.start : this.timelineBoundaries.end;
        this.initializeTimeline(this.pageNumber * 2, false);
    }

    public getBlockStart(block: Task[]): Date {
        return block.sort((first, second) => first.start.getTime() - second.start.getTime())[0].start;
    }

    public getBlockEnd(block: Task[]): Date {
        return block.sort((first, second) => second.end.getTime() - first.end.getTime())[0].end;
    }

    public changeBlockExpansion(block: TasksBlock) {
        block.expanded = !block.expanded;
    }

    private initializeTimeline(pageNumber?: number, scroll: boolean = true): void {
        this.pageNumber = pageNumber ?? this.defaultPageNumber;
        this.timelineBoundaries = getTimelineBoundaries(this.currentDate, this.timelineScale, this.pageNumber);
        this.separateTasksByRows(this.getVisibleTasks());
        this.timelineSubsections = getTimelineSubsections(this.timelineBoundaries, this.timelineScale);
        this.timelineSections = getTimelineSections(this.timelineBoundaries, this.timelineScale);
        if (this.timelineScroll && scroll) {
            this.scrollTo(this.currentDate);
        }
    }

    private initializeTimelineWidthObserver(): ResizeObserver {
        let observer = new ResizeObserver(entries => {
            for (let entry of entries) {
                if (this.pageNumber != this.defaultPageNumber) {
                    this.scrollTo(this.returnDate);
                }
            }
        });

        observer.observe(this.timelineContainer.nativeElement);

        return observer;
    }

    private isVisible(task: Task): boolean {
        return this.getColumn(task.end) - this.getColumn(task.start) >= this.minimumColumnNumber;
    }

    private scrollTo(date: Date): void {
        const elementRect = this.timelineScroll.nativeElement.getBoundingClientRect();
        let datePosition = elementRect.width * (this.pageNumber * this.getDateRatio(date) - 0.5);
        this.timelineScroll.nativeElement.scrollTo(datePosition, 0);
    }

    private getTimelineLength(): number {
        return this.timelineBoundaries.end.getTime() - this.timelineBoundaries.start.getTime();
    }

    private getDateRatio(date: Date): number {
        return (date.getTime() - this.timelineBoundaries.start.getTime()) / this.getTimelineLength();
    }

    private separateTasksByRows(tasks: Task[]): void {
        let tasksByRows: Task[][] = Array.from(Array(this.taskRowsNumber), () => []);
        let exceeding: Task[][] = [];
        for (let task of tasks) {
            let inserted = false;
            for (let i = 0; i < tasksByRows.length && !inserted; i++) {
                if (!tasksByRows[i].find(t => isBetween(t.end, task.start, task.end) ||
                    isBetween(task.end, t.start, t.end))) {
                    tasksByRows[i].push(task);
                    inserted = true;
                }
            }
            if (!inserted) {
                this.addToExceeding(exceeding, task);
            }
        }

        this.tasksByRows = tasksByRows;
        this.exceedingTaskBlocks = [];
        if (exceeding.length > 0) {
            this.exceedingTaskBlocks = this.getExceedingBlocks(exceeding);
        }
    }

    private getExceedingBlocks(exceeding: Task[][]) {
        let exceedingTaskBlocks: Task[][] = [];
        exceeding = exceeding.sort(
            (firstBlock, secondBlock) => this.getBlockStart(firstBlock).getTime() - this.getBlockStart(secondBlock).getTime()
        );
        let lastBlockIndex = 0;
        exceedingTaskBlocks = [exceeding[0]];
        for (let i = 1; i < exceeding.length; i++) {
            if (this.getBlockEnd(exceedingTaskBlocks[lastBlockIndex]) > this.getBlockStart(exceeding[i])) {
                exceedingTaskBlocks[lastBlockIndex] = exceedingTaskBlocks[lastBlockIndex].concat(exceeding[i]);
            } else {
                exceedingTaskBlocks.push(exceeding[i]);
                lastBlockIndex++;
            }
        }
        return exceedingTaskBlocks.map(tasks => ({ tasks, expanded: false }));
    }

    private addToExceeding(exceeding: Task[][], task: Task): void {
        let inserted = false;
        for (let i = 0; i < exceeding.length && !inserted; i++) {
            let block = exceeding[i];
            if (isBetween(task.end, this.getBlockStart(block), this.getBlockEnd(block)) ||
                isBetween(this.getBlockEnd(block), task.start, task.end)) {
                block.push(task);
                inserted = true;
            }
        }
        if (!inserted) {
            exceeding.push([task]);
        }
    }
}

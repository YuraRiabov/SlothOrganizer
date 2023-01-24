import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { addHours, differenceInHours } from 'date-fns';

import { BaseComponent } from '@shared/components/base/base.component';
import { Task } from '#types/dashboard/tasks/task';
import { TaskBlock } from '#types/dashboard/timeline/task-block';
import { TaskStatus } from '#types/dashboard/timeline/enums/task-status';
import { Timeline } from '#types/dashboard/timeline/timeline';
import { TimelineCreator } from '@utils/timeline/timeline-creator';
import { TimelineScale } from '#types/dashboard/timeline/enums/timeline-scale';
import { TimelineSection } from '#types/dashboard/timeline/timeline-section';

@Component({
    selector: 'so-timeline',
    templateUrl: './timeline.component.html',
    styleUrls: ['./timeline.component.sass']
})
export class TimelineComponent extends BaseComponent implements OnInit, AfterViewInit, OnDestroy {
    public readonly defaultPageNumber = 12;
    public readonly completedTask = TaskStatus.Completed;
    public readonly failedTask = TaskStatus.Failed;
    public readonly inProgressTask = TaskStatus.InProgress;
    public readonly toDoTask = TaskStatus.ToDo;
    private timelineScale: TimelineScale = TimelineScale.Day;
    private currentDate: Date = new Date();
    private returnDate: Date = this.currentDate;
    private timelineResizeObserver!: ResizeObserver;
    private _tasks: Task[] = [];

    @Input() set date(value: Date) {
        this.currentDate = value;
        this.initializeTimeline();
    }
    @Input() public set scale(value: TimelineScale) {
        this.timelineScale = value;
        this.initializeTimeline();
    }

    @Input() public set tasks(tasks: Task[]) {
        this._tasks = tasks;
        this.initializeTimeline();
    }

    @Output() scaleIncreased = new EventEmitter<Date>();
    @Output() blockClicked = new EventEmitter<TaskBlock>();

    @ViewChild('timelineScroll') timelineScroll!: ElementRef;
    @ViewChild('timelineContainer') timelineContainer!: ElementRef;

    public timeline!: Timeline;

    constructor(private timelineCreator: TimelineCreator) {
        super();
    }

    override ngOnDestroy(): void {
        super.ngOnDestroy();
        this.timelineResizeObserver.disconnect();
    }

    ngAfterViewInit(): void {
        this.scrollTo(this.currentDate);
        this.timelineResizeObserver = this.initializeTimelineWidthObserver();
    }

    ngOnInit(): void {
        this.initializeTimeline();
    }

    public increaseScale(section: TimelineSection): void {
        if (this.timelineScale !== TimelineScale.Day) {
            const date = addHours(section.start, differenceInHours(section.end, section.start) / 2);
            this.scaleIncreased.emit(date);
        }
    }

    public loadMore(left: boolean): void {
        this.returnDate = left ? this.timeline.boundaries.start : this.timeline.boundaries.end;
        this.initializeTimeline(this.timeline.pageNumber * 2, false);
    }

    public chooseTask(taskBlock: TaskBlock): void {
        this.blockClicked.emit(taskBlock);
    }

    private initializeTimeline(pageNumber?: number, scroll: boolean = true): void {
        const newPageNumber = pageNumber ?? this.defaultPageNumber;
        this.timeline = this.timelineCreator.create(this.currentDate, this.timelineScale, newPageNumber, this._tasks);
        if (this.timelineScroll && scroll) {
            this.scrollTo(this.currentDate);
        }
    }

    private initializeTimelineWidthObserver(): ResizeObserver {
        const observer = new ResizeObserver(entries => {
            for (const entry of entries) {
                if (this.timeline.pageNumber != this.defaultPageNumber) {
                    this.scrollTo(this.returnDate);
                }
            }
        });

        observer.observe(this.timelineContainer.nativeElement);

        return observer;
    }

    private scrollTo(date: Date): void {
        const elementRect = this.timelineScroll.nativeElement.getBoundingClientRect();
        const datePosition =
            elementRect.width *
            (this.timeline.pageNumber * this.timelineCreator.getDateRatio(date) - 0.5);
        this.timelineScroll.nativeElement.scrollTo(datePosition, 0);
    }
}

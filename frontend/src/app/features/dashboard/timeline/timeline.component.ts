import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { addHours, differenceInHours } from 'date-fns';

import { Task } from '#types/tasks/task';
import { TasksBlock } from '#types/tasks/timeline/tasks-block';
import { Timeline } from '#types/tasks/timeline/timeline';
import { TimelineCreator } from '@utils/timeline/timeline-creator';
import { TimelineScale } from '#types/tasks/timeline/enums/timeline-scale';
import { TimelineSection } from '#types/tasks/timeline/timeline-section';

@Component({
    selector: 'so-timeline',
    templateUrl: './timeline.component.html',
    styleUrls: ['./timeline.component.sass']
})
export class TimelineComponent implements OnInit, AfterViewInit, OnDestroy {
    public readonly defaultPageNumber = 12;
    private timelineScale: TimelineScale = TimelineScale.Day;
    private currentDate: Date = new Date();
    private returnDate: Date = this.currentDate;
    private timelineResizeObserver!: ResizeObserver;

    @Input() set date(value: Date) {
        this.currentDate = value;
        this.initializeTimeline();
    }
    @Input() public tasks: TaskBlock[] = [];
    @Input() public set scale(value: TimelineScale) {
        this.timelineScale = value;
        this.initializeTimeline();
    }

    @Output() scaleIncreased = new EventEmitter<Date>();

    @ViewChild('timelineScroll') timelineScroll!: ElementRef;
    @ViewChild('timelineContainer') timelineContainer!: ElementRef;

    public timeline!: Timeline;

    constructor(private timelineCreator: TimelineCreator) { }

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

    public changeBlockExpansion(block: TasksBlock) {
        block.expanded = !block.expanded;
    }

    private initializeTimeline(pageNumber?: number, scroll: boolean = true): void {
        const newPageNumber = pageNumber ?? this.defaultPageNumber;
        this.timeline = this.timelineCreator.create(this.currentDate, this.timelineScale, newPageNumber, this.tasks);
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

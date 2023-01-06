import { Component, ElementRef, EventEmitter, HostListener, Input, Output } from '@angular/core';

import { TaskBlock } from '#types/dashboard/timeline/task-block';

@Component({
    selector: 'so-exceeding-tasks',
    templateUrl: './exceeding-tasks.component.html',
    styleUrls: ['./exceeding-tasks.component.sass']
})
export class ExceedingTasksComponent {
    private clickCount : number = 0;
    @Output() private clickedOff = new EventEmitter();
    @Output() public blockClicked = new EventEmitter<TaskBlock>();
    @Input() public tasks: TaskBlock[] = [];

    @HostListener('document:click', ['$event'])
    clickout(event: MouseEvent) {
        if(!this.elementRef.nativeElement.contains(event.target)) {
            this.clickCount++;
            if (this.clickCount > 1) {
                this.clickedOff.emit();
            }
        }
    }

    constructor(private elementRef: ElementRef) { }

}
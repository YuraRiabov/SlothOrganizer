import { Component, ElementRef, EventEmitter, HostListener, Input, Output } from '@angular/core';

import { Task } from '#types/tasks/task';

@Component({
    selector: 'app-exceeding-tasks',
    templateUrl: './exceeding-tasks.component.html',
    styleUrls: ['./exceeding-tasks.component.sass']
})
export class ExceedingTasksComponent {
    private clickCount : number = 0;
    @Output() private clickedOff = new EventEmitter();
    @Input() public tasks: Task[] = [];

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
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
    selector: 'so-calendar-tab',
    templateUrl: './calendar-tab.component.html',
    styleUrls: ['./calendar-tab.component.sass']
})
export class CalendarTabComponent{
    @Input() public connectedCalendar: string | null = null;

    @Output() public attachCalendar = new EventEmitter<void>();
    @Output() public detachCalendar = new EventEmitter<void>();
}

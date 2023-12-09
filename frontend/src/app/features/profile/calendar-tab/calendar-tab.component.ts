import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
    selector: 'so-calendar-tab',
    templateUrl: './calendar-tab.component.html',
    styleUrls: ['./calendar-tab.component.sass']
})
export class CalendarTabComponent{

    @Output() public attachCalendar = new EventEmitter<void>();

}

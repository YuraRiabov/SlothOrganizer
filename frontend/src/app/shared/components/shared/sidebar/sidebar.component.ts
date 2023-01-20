import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'so-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.sass']
})
export class SidebarComponent {
    @Output() closed = new EventEmitter();
}

import { Component, ElementRef, EventEmitter, HostListener, Input, Output } from '@angular/core';

import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';

@Component({
    selector: 'app-dashboard-sidebar',
    templateUrl: './dashboard-sidebar.component.html',
    styleUrls: ['./dashboard-sidebar.component.sass']
})
export class DashboardSidebarComponent {

    @Input() public type: SidebarType = SidebarType.Create;
    @Output() public clickedOff = new EventEmitter();

    @HostListener('document:click', ['$event'])
    clickout(event: MouseEvent) {
        const left = this.elementRef.nativeElement.getBoundingClientRect().left;
        if(event.clientX < left) {
            this.clickedOff.emit();
        }
    }

    constructor(private elementRef: ElementRef) { }
}

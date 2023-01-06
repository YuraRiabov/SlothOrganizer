import { AfterViewChecked, Component, ElementRef, EventEmitter, HostListener, Input, Output } from '@angular/core';

import { SidebarType } from '#types/dashboard/timeline/enums/sidebar-type';

@Component({
    selector: 'so-dashboard-sidebar',
    templateUrl: './dashboard-sidebar.component.html',
    styleUrls: ['./dashboard-sidebar.component.sass']
})
export class DashboardSidebarComponent implements AfterViewChecked {
    private viewInitialized: boolean = false;
    @Input() public type: SidebarType = SidebarType.Create;
    @Output() public clickedOff = new EventEmitter();

    @HostListener('document:mousedown', ['$event'])
    clickout(event: MouseEvent) {
        const left = this.elementRef.nativeElement.getBoundingClientRect().left;
        if(event.clientX < left && this.viewInitialized) {
            this.clickedOff.emit();
        }
    }

    constructor(private elementRef: ElementRef) { }

    ngAfterViewChecked(): void {
        this.viewInitialized = true;
    }
}

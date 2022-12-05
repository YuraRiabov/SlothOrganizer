import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject, takeUntil } from 'rxjs';

@Component({
    selector: 'app-base',
    template: ''
})
export class BaseComponent implements OnDestroy {
    protected destroyed$ = new Subject<void>;

    readonly untilThis = <T>(source: Observable<T>) => source.pipe(takeUntil(this.destroyed$));

    ngOnDestroy(): void {
        this.destroyed$.next();
        this.destroyed$.complete();
    }
}

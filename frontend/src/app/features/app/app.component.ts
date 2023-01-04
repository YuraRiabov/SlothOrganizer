import { Component } from '@angular/core';
import { LoadingService } from '@shared/services/loading.service';
import { Observable } from 'rxjs';

@Component({
    selector: 'so-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.sass']
})
export class AppComponent {
    public title = 'Sloth Organizer';
    public loading$ : Observable<boolean> = this.loader.loading$;

    constructor(private loader: LoadingService) {}
}

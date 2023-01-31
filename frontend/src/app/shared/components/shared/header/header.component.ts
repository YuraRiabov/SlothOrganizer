import { Component, OnInit } from '@angular/core';

import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { logoutAction } from '@store/actions/hydration.actions';
import { profileRoute } from '@shared/routes/routes';
import { selectAvatarUrl } from '@store/selectors/auth.selectors';

@Component({
    selector: 'so-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.sass']
})
export class HeaderComponent implements OnInit {
    public avatar$?: Observable<string | undefined>;

    constructor(private router: Router, private store: Store) { }

    ngOnInit(): void {
        this.avatar$ = this.store.select(selectAvatarUrl);
    }

    public goToProfile(): void {
        this.router.navigate([profileRoute]);
    }

    public logout(): void {
        this.store.dispatch(logoutAction());
    }
}

import * as profileActions from '@store/actions/profile.actions';

import { Component, OnInit } from '@angular/core';
import { selectInvalidPassword, selectUser } from '@store/selectors/auth.selectors';

import { Observable } from 'rxjs';
import { PasswordUpdate } from '#types/user/password-update';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { User } from '#types/user/user';
import { dashboardRoute } from '@shared/routes/routes';

@Component({
    selector: 'so-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.sass']
})
export class ProfileComponent implements OnInit {
    public user$?: Observable<User>;
    public incorrectPassword$?: Observable<boolean | undefined>;

    constructor(private store: Store, private router: Router) { }

    ngOnInit(): void {
        this.user$ = this.store.select(selectUser);
        this.incorrectPassword$ = this.store.select(selectInvalidPassword);
        this.loadCalendar();
    }

    public updateAvatar(formData: FormData | null): void {
        if (formData) {
            this.store.dispatch(profileActions.uploadAvatar({ image: formData }));
        } else {
            this.store.dispatch(profileActions.deleteAvatar());
        }
    }

    public updateFirstName(firstName: string): void {
        this.store.dispatch(profileActions.updateFirstName({ firstName }));
    }

    public updateLastName(lastName: string): void {
        this.store.dispatch(profileActions.updateLastName({ lastName }));
    }

    public updatePassword(passwordUpdate: PasswordUpdate): void {
        this.store.dispatch(profileActions.updatePassword({ passwordUpdate }));
    }

    public attachCalendar(): void {
        this.store.dispatch(profileActions.attachCalendar());
    }

    public detachCalendar(): void {
        this.store.dispatch(profileActions.deleteCalendar());
    }

    public loadCalendar(): void {
        this.store.dispatch(profileActions.getCalendar());
    }

    public goToDashboard(): void {
        this.router.navigate([dashboardRoute]);
    }
}

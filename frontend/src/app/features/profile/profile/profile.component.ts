import * as profileActions from '@store/actions/profile.actions';

import { Component, OnInit } from '@angular/core';

import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { User } from '#types/user/user';
import { selectUser } from '@store/selectors/auth.selectors';

@Component({
    selector: 'so-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.sass']
})
export class ProfileComponent implements OnInit {
    public user$?: Observable<User>;

    constructor(private store: Store) { }

    ngOnInit(): void {
        this.user$ = this.store.select(selectUser);
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
}

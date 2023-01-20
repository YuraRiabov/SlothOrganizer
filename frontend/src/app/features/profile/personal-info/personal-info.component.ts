import { Component, EventEmitter, Input, Output } from '@angular/core';

import { FormControl } from '@angular/forms';
import { User } from '#types/user/user';
import { getNameValidators } from '@utils/validators/user-validators.helper';

@Component({
    selector: 'so-personal-info',
    templateUrl: './personal-info.component.html',
    styleUrls: ['./personal-info.component.sass']
})
export class PersonalInfoComponent {
    @Input() public set user(value: User) {
        this.avatarUrl = value.avatarUrl;
        this.buildControls(value);
    }
    @Output() public avatarUpdated = new EventEmitter<FormData | null>();
    @Output() public firstNameUpdated = new EventEmitter<string>();
    @Output() public lastNameUpdated = new EventEmitter<string>();

    public firstNameControl!: FormControl;
    public lastNameControl!: FormControl;

    public avatarUrl?: string;

    public updateAvatar(event: Event): void {
        const file: File = (<HTMLInputElement>event.target).files![0];

        if(file) {
            const formData = new FormData();
            formData.append('imagedata', file);
            this.avatarUpdated.emit(formData);
        }
    }

    public deleteAvatar(): void {
        this.avatarUpdated.emit(null);
    }

    public updateFirstName(): void {
        if (this.firstNameControl.valid) {
            this.firstNameUpdated.emit(this.firstNameControl.value!);
        }
    }

    public updateLastName(): void {
        if (this.lastNameControl.valid) {
            this.firstNameUpdated.emit(this.lastNameControl.value!);
        }
    }

    private buildControls(user: User) {
        this.firstNameControl = new FormControl(user.firstName, getNameValidators());
        this.lastNameControl = new FormControl(user.lastName, getNameValidators());
    }
}

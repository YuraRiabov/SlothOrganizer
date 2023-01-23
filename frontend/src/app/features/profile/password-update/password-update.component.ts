import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { getPasswordValidators, passwordMatchingValidator } from '@utils/validators/user-validators.helper';

import { PasswordUpdate } from '#types/user/password-update';
import { hasLengthErrors } from '@utils/validators/common-validators';

@Component({
    selector: 'so-password-update',
    templateUrl: './password-update.component.html',
    styleUrls: ['./password-update.component.sass']
})
export class PasswordUpdateComponent {
    @Input() public set incorrectPassword(value: boolean | null | undefined) {
        if (value) {
            this.passwordUpdateGroup.get('oldPassword')?.setErrors({ incorrectPassword: true });
        }
    }
    @Output() public updatePassword = new EventEmitter<PasswordUpdate>();

    public passwordUpdateGroup!: FormGroup;

    constructor() {
        this.passwordUpdateGroup = this.buildFormGroup();
    }

    public resetPassword(): void {
        this.updatePassword.emit({
            oldPassword: this.passwordUpdateGroup.get('oldPassword')?.value,
            password: this.passwordUpdateGroup.get('password')?.value
        });
    }

    public hasLengthErrors(controlName: string): boolean {
        return hasLengthErrors(this.passwordUpdateGroup, controlName);
    }

    private buildFormGroup() : FormGroup {

        const oldPasswordControl = new FormControl('', Validators.required);

        const passwordControl = new FormControl('', getPasswordValidators());

        const repeatPasswordControl = new FormControl('', Validators.required);

        return new FormGroup({
            oldPassword: oldPasswordControl,
            password: passwordControl,
            repeatPassword: repeatPasswordControl
        }, {
            validators: passwordMatchingValidator()
        });
    }

}

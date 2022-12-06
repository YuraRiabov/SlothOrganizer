import { AbstractControl, ValidatorFn, Validators } from '@angular/forms';

export const getNameValidators = () => [
    Validators.required,
    Validators.minLength(2),
    Validators.maxLength(30)
];

export const getPasswordValidators = () => [
    Validators.required,
    Validators.minLength(8),
    Validators.maxLength(16),
    Validators.pattern('([0-9].*[a-zA-Z])|([a-zA-Z].*[0-9])')
];

export const getEmailValidators = () => [
    Validators.required,
    Validators.email
];

export const passwordMatchingValidator = () : ValidatorFn => {
    return (group: AbstractControl) => {
        const passwordControl = group.get('password');
        const repeatPasswordControl = group.get('repeatPassword');
        if ( passwordControl?.value !== repeatPasswordControl?.value ) {
            repeatPasswordControl?.setErrors({ passwordMismatch: true });
        }
        return null;
    };
};
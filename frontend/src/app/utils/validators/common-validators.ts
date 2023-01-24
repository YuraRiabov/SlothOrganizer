import { FormGroup } from '@angular/forms';

export const hasLengthErrors =  (group: FormGroup, controlName: string): boolean => {
    return group.get(controlName)?.hasError('minlength') || !!group.get(controlName)?.hasError('maxlength');
};
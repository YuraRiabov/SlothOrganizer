import { createAction, props } from '@ngrx/store';

import { PasswordUpdate } from '#types/user/password-update';
import { Calendar } from '#types/user/calendar';

export const uploadAvatar = createAction(
    '[Profile page] Upload image',
    props<{ image: FormData }>()
);

export const uploadAvatarSuccess = createAction(
    '[Profile page] Upload avatar success',
    props<{ url: string }>()
);

export const deleteAvatar = createAction(
    '[Profile page] Delete avatar'
);

export const updateFirstName = createAction(
    '[Profile page] Update first name',
    props<{ firstName: string }>()
);

export const updateLastName = createAction(
    '[Profile page] Update last name',
    props<{ lastName: string }>()
);

export const updatePassword = createAction(
    '[Profile page] Update password',
    props<{ passwordUpdate: PasswordUpdate }>()
);

export const updatePasswordSuccess = createAction(
    '[Profile page] Update password success'
);

export const updatePasswordFailure = createAction(
    '[Profile page] Update password failure'
);

export const attachCalendar = createAction(
    '[Profile page] Attach Calendar'
);

export const getCalendar = createAction(
    '[Profile page] Get Calendar'
);

export const getCalendarSuccess = createAction(
    '[Profile page] Get Calendar Success',
    props<{ calendar: Calendar | null }>()
);

export const deleteCalendar = createAction(
    '[Profile page] Delete calendar'
);
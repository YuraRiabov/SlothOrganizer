import { MetaReducer } from '@ngrx/store';
import { hydrationMetaReducer } from './hydration.reducer';
import { logoutMetaReducer } from './logout.reducer';

export const metaReducers: MetaReducer[] = [hydrationMetaReducer, logoutMetaReducer];
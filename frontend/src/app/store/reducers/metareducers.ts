import { MetaReducer } from '@ngrx/store';
import { hydrationMetaReducer } from './hydration.reducer';

export const metaReducers: MetaReducer[] = [hydrationMetaReducer];
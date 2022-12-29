import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { EffectsModule } from '@ngrx/effects';
import { ErrorInterceptor } from '@shared/interceptors/error.interceptor';
import { HydrationEffects } from '@store/effects/meta/hydration.effects';
import { LoadingInterceptor } from '@shared/interceptors/loading.interceptor';
import { MaterialModule } from '@shared/material/material.module';
import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { TokenInterceptor } from '@shared/interceptors/token.interceptor';
import { authPageReducer } from '@store/reducers/auth-page.reducers';
import { metaReducers } from '@store/reducers/meta/metareducers';

@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        StoreModule.forRoot({ authState: authPageReducer }, { metaReducers }),
        EffectsModule.forRoot([HydrationEffects]),
        BrowserAnimationsModule,
        HttpClientModule,
        MaterialModule
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true},
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
        { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true}
    ],
    bootstrap: [AppComponent]
})
export class AppModule {}

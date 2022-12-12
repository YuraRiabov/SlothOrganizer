import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './features/app.component';
import { AppRoutingModule } from './routes/app-routing.module';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { ErrorInterceptor } from './shared/interceptors/error.interceptor';
import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { TokenInterceptor } from './shared/interceptors/token.interceptor';
import { authPageReducer } from './store/reducers/auth-page.reducers';
import { metaReducers } from './store/reducers/metareducers';
import { authPageReducer } from '../../store/reducers/auth-page.reducers';

@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        StoreModule.forRoot({ authState: authPageReducer }, { metaReducers }),
        BrowserAnimationsModule,
        HttpClientModule
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true},
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}
    ],
    bootstrap: [AppComponent]
})
export class AppModule {}

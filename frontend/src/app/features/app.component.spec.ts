/* eslint-disable no-undef */

import { AppComponent } from './app.component';
import { AppRoutingModule } from '@routes/app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import { TestBed } from '@angular/core/testing';
import { authPageReducer } from '@store/reducers/auth-page.reducers';

describe('AppComponent', () => {
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                BrowserModule,
                AppRoutingModule,
                StoreModule.forRoot({ authState: authPageReducer }, {}),
                BrowserAnimationsModule,
                HttpClientModule
            ],
            declarations: [AppComponent]
        }).compileComponents();
    });

    it('should create the app', () => {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.componentInstance;
        expect(app).toBeTruthy();
    });

    it(`should have as title 'Sloth Organizer'`, () => {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.componentInstance;
        expect(app.title).toEqual('Sloth Organizer');
    });
});

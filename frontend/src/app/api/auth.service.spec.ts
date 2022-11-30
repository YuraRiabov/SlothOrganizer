import { AuthService } from './auth.service';
/* eslint-disable no-undef */
import { TestBed } from '@angular/core/testing';

describe('UserService', () => {
    let service: AuthService;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(AuthService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});

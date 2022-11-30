import { HttpInternalService } from './http-internal.service';
/* eslint-disable no-undef */
import { TestBed } from '@angular/core/testing';

describe('HttpInternalService', () => {
    let service: HttpInternalService;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(HttpInternalService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});

import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, finalize } from 'rxjs';

import { Injectable } from '@angular/core';
import { LoadingService } from '@shared/services/loading.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

    totalRequests = 0;
    completedRequests = 0;

    constructor(private loader: LoadingService) { }

    intercept(
        request: HttpRequest<unknown>,
        next: HttpHandler
    ): Observable<HttpEvent<unknown>> {

        this.loader.show();
        this.totalRequests++;

        return next.handle(request).pipe(
            finalize(() => {
                this.completedRequests++;

                if (this.completedRequests === this.totalRequests) {
                    this.loader.hide();
                    this.completedRequests = 0;
                    this.totalRequests = 0;
                }
            })
        );
    }
}
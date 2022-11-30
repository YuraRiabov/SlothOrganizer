import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class HttpInternalService {
    private apiUrl: string = environment.apiUrl;

    constructor(private http: HttpClient) {}

    public getRequest<T>(url: string, httpParams?: any): Observable<T> {
        return this.http.get<T>(this.buildUrl(url), { params: httpParams });
    }

    public getStringRequest(url: string, httpParams?: any): Observable<string> {
        return this.http.get(this.buildUrl(url), {
            params: httpParams,
            responseType: 'text'
        });
    }

    public deleteRequest<T>(url: string, httpParams?: any): Observable<T> {
        return this.http.delete<T>(this.buildUrl(url), {
            params: httpParams
        });
    }

    public postRequest<T>(
        url: string,
        payload: object,
        httpParams?: any
    ): Observable<T> {
        return this.http.post<T>(this.buildUrl(url), payload, {
            params: httpParams
        });
    }

    public putRequest<T>(
        url: string,
        payload: object,
        httpParams?: any
    ): Observable<T> {
        return this.http.put<T>(this.buildUrl(url), payload, {
            params: httpParams
        });
    }

    private buildUrl(url: string) {
        if (url.includes('http')) {
            return url;
        }
        return this.apiUrl + url;
    }
}

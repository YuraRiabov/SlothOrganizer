import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

export abstract class HttpService {
    private apiUrl: string = environment.apiUrl;

    protected readonly abstract controllerUri: string;

    constructor(private http: HttpClient) {}

    public get<T>(url: string, httpParams?: any): Observable<T> {
        return this.http.get<T>(this.buildUrl(url), { params: httpParams });
    }

    public getString(url: string, httpParams?: any): Observable<string> {
        return this.http.get(this.buildUrl(url), {
            params: httpParams,
            responseType: 'text'
        });
    }

    public delete<T>(url: string, httpParams?: any): Observable<T> {
        return this.http.delete<T>(this.buildUrl(url), {
            params: httpParams
        });
    }

    public post<T>(
        url: string,
        payload?: object,
        httpParams?: any
    ): Observable<T> {
        return this.http.post<T>(this.buildUrl(url), payload, {
            params: httpParams
        });
    }

    public put<T>(
        url: string,
        payload?: object,
        httpParams?: any
    ): Observable<T> {
        return this.http.put<T>(this.buildUrl(url), payload, {
            params: httpParams
        });
    }

    private buildUrl(url: string) : string {
        return this.apiUrl + this.controllerUri + url;
    }
}

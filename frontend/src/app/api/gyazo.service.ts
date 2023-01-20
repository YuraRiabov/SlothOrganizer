import { Observable, map } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class GyazoService {
    constructor(private http: HttpClient) { }

    public upload(image: FormData): Observable<string> {
        return this.http.post<{ url: string }>(this.addToken(environment.gyazo.uploadUri), image, {
            headers: {
                'Access-Control-Allow-Origin': '*'
            }
        }).pipe(
            map(response => response.url)
        );
    }

    private addToken(urlString: string): string {
        const url = new URL(urlString);
        url.searchParams.append('access_token', environment.gyazo.token);
        return url.toString();
    }
}
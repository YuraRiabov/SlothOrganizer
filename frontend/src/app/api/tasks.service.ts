import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { Observable } from 'rxjs';
import { Task } from '#types/dashboard/tasks/task';

@Injectable({
    providedIn: 'root'
})
export class TasksService {
    private readonly baseUri: string = '/tasks';

    constructor(private http: HttpService) { }

    public create(newTask: NewTask): Observable<Task> {
        return this.http.post<Task>(this.baseUri, newTask);
    }
}

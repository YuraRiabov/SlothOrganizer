import { HttpClient } from '@angular/common/http';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { Observable } from 'rxjs';
import { Task } from '#types/dashboard/tasks/task';
import { TaskUpdate } from '#types/dashboard/tasks/task-update';

@Injectable({
    providedIn: 'root'
})
export class TasksService extends HttpService {
    protected override readonly controllerUri: string = '/tasks';

    constructor(http: HttpClient) {
        super(http);
    }

    public create(newTask: NewTask): Observable<Task> {
        return this.post<Task>('', newTask);
    }

    public load(dashboardId: number): Observable<Task[]> {
        return this.get<Task[]>(`/${dashboardId}`);
    }

    public update(taskUpdate: TaskUpdate): Observable<Task> {
        return this.put('', taskUpdate);
    }

    public export(dashboardId: number) : Observable<void> {
        return this.post(`/export/${dashboardId}`);
    }
}

import { HttpClient } from '@angular/common/http';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { NewTask } from '#types/dashboard/tasks/new-task';
import { Observable } from 'rxjs';
import { Task } from '#types/dashboard/tasks/task';
import { TaskCompletion } from '#types/dashboard/tasks/task-completion';
import { TaskExport } from '#types/dashboard/tasks/task-export';

@Injectable({
    providedIn: 'root'
})
export class TaskComletionService extends HttpService {
    protected override readonly controllerUri: string = '/task-completions';

    constructor(http: HttpClient) {
        super(http);
    }

    public update(taskCompletion: TaskCompletion) : Observable<TaskCompletion> {
        return this.put('', taskCompletion);
    }

    public export(task: TaskExport) : Observable<void> {
        return this.post('/export', task);
    }

    public remove(id: number) : Observable<null> {
        return this.delete(`/${id}`);
    }
}

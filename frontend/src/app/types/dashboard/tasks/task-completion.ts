export interface TaskCompletion {
    id: number;
    taskId: number;
    start: Date;
    end: Date;
    isSuccessful: boolean;
    isExported?: boolean;
    lastEdited?: Date;
}
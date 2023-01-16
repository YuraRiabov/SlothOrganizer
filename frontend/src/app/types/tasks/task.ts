export interface Task {
    start: Date;
    end: Date;
    title: string;
    startColumn?: number;
    endColumn?: number;
}
import { Calendar } from "./calendar";

export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  avatarUrl?: string;
  calendar: Calendar | null;
}

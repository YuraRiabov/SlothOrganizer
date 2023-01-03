import { TaskRepeatingPeriod } from '#types/dashboard/tasks/enums/task-repeating-period';

export const getRepeatingPeriods = () => [
    TaskRepeatingPeriod.None,
    TaskRepeatingPeriod.Day,
    TaskRepeatingPeriod.Week,
    TaskRepeatingPeriod.Month,
    TaskRepeatingPeriod.Year
];
using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;
using SlothOrganizer.Services.Abstractions.Tasks;

namespace SlothOrganizer.Services.Tasks
{
    public class TaskCompletionPeriodConverter : ITaskCompletionPeriodConverter
    {
        public TimeSpan GetLength(TaskRepeatingPeriod period)
        {
            return period switch
            {
                TaskRepeatingPeriod.None => TimeSpan.MaxValue,
                TaskRepeatingPeriod.Day => TimeSpan.FromDays(1),
                TaskRepeatingPeriod.Week => TimeSpan.FromDays(7),
                TaskRepeatingPeriod.Month => TimeSpan.FromDays(28),
                TaskRepeatingPeriod.Year => TimeSpan.FromDays(365),
                _ => TimeSpan.MinValue
            };
        }

        public TaskRepeatingPeriod GetRepeatingPeriod(TimeSpan repeatsDifference)
        {
            if (repeatsDifference < TimeSpan.FromDays(3))
            {
                return TaskRepeatingPeriod.Day;
            }
            if (repeatsDifference < TimeSpan.FromDays(14))
            {
                return TaskRepeatingPeriod.Week;
            }
            if (repeatsDifference < TimeSpan.FromDays(40))
            {
                return TaskRepeatingPeriod.Month;
            }
            return TaskRepeatingPeriod.Year;
        }
    }
}

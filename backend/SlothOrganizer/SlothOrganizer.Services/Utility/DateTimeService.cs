using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Utility
{
    public class DateTimeService : IDateTimeService
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

        public DateTime Now() => DateTime.Now;
    }
}

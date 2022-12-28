using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;

namespace SlothOrganizer.Services.Abstractions.Utility
{
    public interface IDateTimeService
    {
        DateTime Now();
        TimeSpan GetLength(TaskRepeatingPeriod period);
    }
}

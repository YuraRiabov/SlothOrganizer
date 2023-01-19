using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;

namespace SlothOrganizer.Services.Abstractions.Tasks
{
    public interface ITaskCompletionPeriodConverter
    {
        TimeSpan GetLength(TaskRepeatingPeriod period);
    }
}

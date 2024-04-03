using SlothOrganizer.Contracts.DTO.Tasks.Task;

namespace SlothOrganizer.Services.Abstractions.Tasks
{
    public interface ITaskCompletionService
    {
        Task<IEnumerable<TaskCompletionDto>> Add(IEnumerable<TaskCompletionDto> completions, DateTimeOffset endLimit);
        Task<List<TaskCompletionDto>> Create(NewTaskDto newTask, long taskId);
        Task Delete(long taskId, DateTimeOffset endLimit);
        Task Delete(long id);
        Task<TaskCompletionDto> Update(TaskCompletionDto taskCompletionDto);
        Task Export(ExportTaskCompletionDto completion, long userId);
    }
}

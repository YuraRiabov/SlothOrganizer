using SlothOrganizer.Contracts.DTO.Tasks.Task;

namespace SlothOrganizer.Services.Abstractions.Tasks
{
    public interface ITaskCompletionService
    {
        Task<List<TaskCompletionDto>> Add(List<TaskCompletionDto> completions, DateTimeOffset endRepeating);
        Task<List<TaskCompletionDto>> Create(NewTaskDto newTask, long taskId);
        Task Delete(long taskId, DateTimeOffset endRepeating);
        Task Delete(long id);
        Task<TaskCompletionDto> Update(TaskCompletionDto taskCompletionDto);
    }
}

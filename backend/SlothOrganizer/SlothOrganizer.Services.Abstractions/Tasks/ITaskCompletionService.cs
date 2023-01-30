using SlothOrganizer.Contracts.DTO.Tasks.Task;

namespace SlothOrganizer.Services.Abstractions.Tasks
{
    public interface ITaskCompletionService
    {
        Task<IEnumerable<TaskCompletionDto>> Add(IEnumerable<TaskCompletionDto> completions, DateTimeOffset endRepeating);
        Task<List<TaskCompletionDto>> Create(NewTaskDto newTask, long taskId);
        Task Delete(long taskId, DateTimeOffset endRepeating);
        Task Delete(long id);
        Task<TaskCompletionDto> Update(TaskCompletionDto taskCompletionDto);
    }
}

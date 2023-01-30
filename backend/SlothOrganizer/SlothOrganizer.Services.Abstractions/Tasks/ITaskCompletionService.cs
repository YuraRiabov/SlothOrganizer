using SlothOrganizer.Contracts.DTO.Tasks.Task;

namespace SlothOrganizer.Services.Abstractions.Tasks
{
    public interface ITaskCompletionService
    {
        Task<IEnumerable<TaskCompletionDto>> Add(IEnumerable<TaskCompletionDto> completions, DateTime endRepeating);
        Task<List<TaskCompletionDto>> Create(NewTaskDto newTask, long taskId);
        Task Delete(long taskId, DateTime endRepeating);
        Task Delete(long id);
        Task<TaskCompletionDto> Update(TaskCompletionDto taskCompletionDto);
    }
}

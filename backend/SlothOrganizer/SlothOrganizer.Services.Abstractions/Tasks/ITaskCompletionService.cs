using SlothOrganizer.Contracts.DTO.Tasks.Task;

namespace SlothOrganizer.Services.Abstractions.Tasks
{
    public interface ITaskCompletionService
    {
        Task<List<TaskCompletionDto>> AddLacking(List<TaskCompletionDto> completions, DateTime endRepeating);
        Task<List<TaskCompletionDto>> Create(NewTaskDto newTask, long taskId);
        Task DeleteExceeding(long taskId, DateTime endRepeating);
        Task<TaskCompletionDto> Update(TaskCompletionDto taskCompletionDto);
    }
}

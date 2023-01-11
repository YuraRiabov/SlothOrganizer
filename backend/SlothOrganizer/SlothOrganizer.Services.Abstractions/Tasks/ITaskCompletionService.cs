using SlothOrganizer.Contracts.DTO.Tasks.Task;

namespace SlothOrganizer.Services.Abstractions.Tasks
{
    public interface ITaskCompletionService
    {
        Task<List<TaskCompletionDto>> Create(NewTaskDto newTask, long taskId);
        Task<TaskCompletionDto> Update(TaskCompletionDto taskCompletionDto);
    }
}

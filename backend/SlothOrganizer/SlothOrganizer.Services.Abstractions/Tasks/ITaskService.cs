using SlothOrganizer.Contracts.DTO.Tasks.Task;

namespace SlothOrganizer.Services.Abstractions.Tasks
{
    public interface ITaskService
    {
        Task<TaskDto> Create(NewTaskDto newTask);
        Task<List<TaskDto>> Get(long dashboardId);
    }
}

using SlothOrganizer.Contracts.DTO.Tasks.Task;

namespace SlothOrganizer.Services.Abstractions.Tasks
{
    public interface ITaskService
    {
        Task<TaskDto> Create(NewTaskDto newTask, long? userId = null);
        Task<List<TaskDto>> Get(long dashboardId);
        Task<TaskDto> Update(UpdateTaskDto updateTaskDto, long? userId = null);
        Task Export(long dashboardId, long userId);
    }
}

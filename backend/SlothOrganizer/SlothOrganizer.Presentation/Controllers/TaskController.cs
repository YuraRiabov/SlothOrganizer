using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Presentation.Extensions;
using SlothOrganizer.Services.Abstractions.Tasks;

namespace SlothOrganizer.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<TaskDto> Create(NewTaskDto newTask)
        {
            return await _taskService.Create(newTask, HttpContext.User.GetId());
        }

        [HttpGet("{dashboardId}")]
        public async Task<List<TaskDto>> Get(long dashboardId)
        {
            return await _taskService.Get(dashboardId);
        }

        [HttpPut]
        public async Task<TaskDto> Update([FromBody] UpdateTaskDto updateTaskDto)
        {
            return await _taskService.Update(updateTaskDto, HttpContext.User.GetId());
        }

        [HttpPost("export/{dashboardId}")]
        public async Task Export(long dashboardId)
        {
            await _taskService.Export(dashboardId, HttpContext.User.GetId());
        }
    }
}

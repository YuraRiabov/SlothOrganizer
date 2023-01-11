using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Services.Abstractions.Tasks;

namespace SlothOrganizer.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("task-completions")]
    public class TaskCompletionController : ControllerBase
    {
        private readonly ITaskCompletionService _taskCompletionService;

        public TaskCompletionController(ITaskCompletionService taskCompletionService)
        {
            _taskCompletionService = taskCompletionService;
        }

        [HttpPut("{id}")]
        public async Task<TaskCompletionDto> Update([FromBody] TaskCompletionDto taskCompletionDto)
        {
            return await _taskCompletionService.Update(taskCompletionDto);
        }
    }
}

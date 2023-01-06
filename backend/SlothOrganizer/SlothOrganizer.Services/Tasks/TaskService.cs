using AutoMapper;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Tasks;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly ITaskCompletionService _taskCompletionService;
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        private readonly IDateTimeService _dateTimeService;
        public TaskService(ITaskCompletionService taskCompletionService, IMapper mapper, ITaskRepository taskRepository, IDateTimeService dateTimeService)
        {
            _taskCompletionService = taskCompletionService;
            _mapper = mapper;
            _taskRepository = taskRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task<TaskDto> Create(NewTaskDto newTask)
        {
            if (newTask.End - newTask.Start > _dateTimeService.GetLength(newTask.RepeatingPeriod))
            {
                throw new InvalidPeriodException();
            }
            var task = await _taskRepository.Insert(_mapper.Map<Domain.Entities.Task>(newTask));
            var taskCompletions = await _taskCompletionService.Create(newTask, task.Id);
            var taskDto = _mapper.Map<TaskDto>(task);
            taskDto.TaskCompletions = taskCompletions;
            return taskDto;
        }

        public async Task<List<TaskDto>> Get(long dashboardId)
        {
            var tasks = await _taskRepository.Get(dashboardId);
            return _mapper.Map<List<TaskDto>>(tasks);
        }
    }
}

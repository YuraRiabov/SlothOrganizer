using AutoMapper;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Domain.Entities;
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
        private readonly ITaskCompletionPeriodConverter _taskCompletionPeriodConverter;
        public TaskService(ITaskCompletionService taskCompletionService, IMapper mapper, ITaskRepository taskRepository, ITaskCompletionPeriodConverter taskCompletionPeriodConverter)
        {
            _taskCompletionService = taskCompletionService;
            _mapper = mapper;
            _taskRepository = taskRepository;
            _taskCompletionPeriodConverter = taskCompletionPeriodConverter;
        }

        public async Task<TaskDto> Create(NewTaskDto newTask)
        {
            if (newTask.End - newTask.Start > _taskCompletionPeriodConverter.GetLength(newTask.RepeatingPeriod))
            {
                throw new InvalidPeriodException();
            }
            var task = await _taskRepository.Insert(_mapper.Map<UserTask>(newTask));
            var taskCompletions = await _taskCompletionService.Create(newTask, task.Id);
            var taskDto = _mapper.Map<TaskDto>(task);
            taskDto.TaskCompletions = taskCompletions;
            return taskDto;
        }
    }
}

using AutoMapper;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Tasks;

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

        public async Task<List<TaskDto>> Get(long dashboardId)
        {
            var tasks = await _taskRepository.Get(dashboardId);
            return _mapper.Map<List<TaskDto>>(tasks);
        }

        public async Task<TaskDto> Update(UpdateTaskDto updateTaskDto)
        {
            await _taskCompletionService.Update(updateTaskDto.TaskCompletion);
            var task = _mapper.Map<UserTask>(updateTaskDto.Task);
            var updatedTask = _mapper.Map<TaskDto>(await _taskRepository.Update(task));
            if (updatedTask is null)
            {
                throw new EntityNotFoundException("Task with such id not found");
            } 
            var latestCompletion = updatedTask.TaskCompletions.MaxBy(tc => tc.End);
            if (updateTaskDto.EndRepeating is DateTimeOffset endRepeating && updatedTask.TaskCompletions.Count > 1)
            {
                if (latestCompletion.End > endRepeating)
                {
                    await _taskCompletionService.Delete(updatedTask.Id, endRepeating);
                    updatedTask.TaskCompletions = updatedTask.TaskCompletions
                        .Where(tc => tc.End < updateTaskDto.EndRepeating)
                        .ToList();
                }
                if (latestCompletion.End < endRepeating)
                {
                    var addedCompletions = await _taskCompletionService.Add(updatedTask.TaskCompletions, endRepeating);
                    updatedTask.TaskCompletions.AddRange(addedCompletions);
                }
            }
            return updatedTask;
        }
    }
}

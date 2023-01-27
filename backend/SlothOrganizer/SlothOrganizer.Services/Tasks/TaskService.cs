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
            var updatedTask = await _taskRepository.Update(task);
            var updatedTaskDto = _mapper.Map<TaskDto>(updatedTask);

            if (updatedTaskDto is null)
            {
                throw new EntityNotFoundException("Task with such id not found");
            } 

            if (updateTaskDto.EndRepeating is DateTime endRepeating && updatedTaskDto.TaskCompletions.Count > 1)
            {
                await UpdateCompletions(updatedTaskDto, endRepeating);
            }
            return updatedTaskDto;
        }

        private async Task UpdateCompletions(TaskDto task, DateTime endRepeating)
        {
            var latestCompletion = task.TaskCompletions.MaxBy(tc => tc.End);
            if (latestCompletion.End > endRepeating)
            {
                await DeleteExceedingCompletions(task, endRepeating);
            }
            if (latestCompletion.End < endRepeating)
            {
                await AddLackingCompletions(task, endRepeating);
            }
        }

        private async Task AddLackingCompletions(TaskDto task, DateTime endRepeating)
        {
            var addedCompletions = await _taskCompletionService.Add(task.TaskCompletions, endRepeating);
            task.TaskCompletions.AddRange(addedCompletions);
        }

        private async Task DeleteExceedingCompletions(TaskDto task, DateTime endLimit)
        {
            await _taskCompletionService.Delete(task.Id, endLimit);
            task.TaskCompletions = task.TaskCompletions
                .Where(tc => tc.End < endLimit)
                .ToList();
        }
    }
}

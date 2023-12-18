using System.Diagnostics.CodeAnalysis;
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

        public TaskService(ITaskCompletionService taskCompletionService, IMapper mapper, ITaskRepository taskRepository,
            ITaskCompletionPeriodConverter taskCompletionPeriodConverter)
        {
            _taskCompletionService = taskCompletionService;
            _mapper = mapper;
            _taskRepository = taskRepository;
            _taskCompletionPeriodConverter = taskCompletionPeriodConverter;
        }

        public async Task<TaskDto> Create(NewTaskDto newTask, long? userId = null)
        {
            if (newTask.End - newTask.Start > _taskCompletionPeriodConverter.GetLength(newTask.RepeatingPeriod))
            {
                throw new InvalidPeriodException();
            }

            var task = await _taskRepository.Insert(_mapper.Map<UserTask>(newTask));
            var taskCompletions = await _taskCompletionService.Create(newTask, task.Id);
            var taskDto = _mapper.Map<TaskDto>(task);
            taskDto.TaskCompletions = taskCompletions;

            if (newTask.ShouldExport && userId != null)
            {
                await ExportTaskCompletions(userId.Value, taskCompletions, task.Title);
            }

            return taskDto;
        }

        public async Task<List<TaskDto>> Get(long dashboardId)
        {
            var tasks = await _taskRepository.Get(dashboardId);
            return _mapper.Map<List<TaskDto>>(tasks);
        }

        public async Task<TaskDto> Update(UpdateTaskDto updateTaskDto, long? userId = null)
        {
            await _taskCompletionService.Update(updateTaskDto.TaskCompletion);
            var task = _mapper.Map<UserTask>(updateTaskDto.Task);
            var updatedTask = await _taskRepository.Update(task);
            var updatedTaskDto = _mapper.Map<TaskDto>(updatedTask);

            if (updatedTaskDto is null)
            {
                throw new EntityNotFoundException("Task with such id not found");
            }

            if (updateTaskDto.EndRepeating is DateTimeOffset endRepeating && updatedTaskDto.TaskCompletions.Count > 1)
            {
                await UpdateCompletions(updatedTaskDto, endRepeating);
            }

            if (updateTaskDto.ShouldExport && userId.HasValue)
            {
                var taskCompletionsToExport = (await Get(updatedTask.DashboardId))
                    .First(t => t.Id == updatedTask.Id)
                    .TaskCompletions.Where(tc => !tc.IsExported);

                await ExportTaskCompletions(userId.Value, taskCompletionsToExport, task.Title);
            }

            return updatedTaskDto;
        }

        public async Task Export(long dashboardId, long userId)
        {
            var tasks = await Get(dashboardId);

            foreach (var task in tasks)
            {
                await ExportTaskCompletions(userId, task.TaskCompletions, task.Title);
            }
        }

        private async Task ExportTaskCompletions(long userId, IEnumerable<TaskCompletionDto> taskCompletionsToExport, string taskName)
        {
            foreach (var completion in taskCompletionsToExport.Where(tc => !tc.IsExported))
            {
                await _taskCompletionService.Export(new ExportTaskCompletionDto
                {
                    Id = completion.Id,
                    End = completion.End,
                    Start = completion.Start,
                    TaskName = taskName
                }, userId);
                completion.IsExported = true;
            }
        }

        private async Task UpdateCompletions(TaskDto task, DateTimeOffset endRepeating)
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

        private async Task AddLackingCompletions(TaskDto task, DateTimeOffset endLimit)
        {
            var addedCompletions = await _taskCompletionService.Add(task.TaskCompletions, endLimit);
            task.TaskCompletions.AddRange(addedCompletions);
        }

        private async Task DeleteExceedingCompletions(TaskDto task, DateTimeOffset endLimit)
        {
            await _taskCompletionService.Delete(task.Id, endLimit);
            task.TaskCompletions = task.TaskCompletions
                .Where(tc => tc.End < endLimit)
                .ToList();
        }
    }
}
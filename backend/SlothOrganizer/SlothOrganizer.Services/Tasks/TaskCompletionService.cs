using System.Threading.Tasks;
using AutoMapper;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Tasks;
using SlothOrganizer.Services.Abstractions.Utility;
using Task = System.Threading.Tasks.Task;

namespace SlothOrganizer.Services.Tasks
{
    public class TaskCompletionService : ITaskCompletionService
    {
        private readonly IMapper _mapper;
        private readonly ITaskCompletionRepository _taskCompletionRepository;
        private readonly IDateTimeService _dateTimeService;
        public TaskCompletionService(IMapper mapper, ITaskCompletionRepository taskCompletionRepository, IDateTimeService dateTimeService)
        {
            _mapper = mapper;
            _taskCompletionRepository = taskCompletionRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task<List<TaskCompletionDto>> Create(NewTaskDto newTask, long taskId)
        {
            var taskCompletions = Generate(newTask, taskId);
            return _mapper.Map<List<TaskCompletionDto>>(await _taskCompletionRepository.Insert(taskCompletions));
        }

        public async Task<TaskCompletionDto> Update(TaskCompletionDto taskCompletionDto)
        {
            var taskCompletion = _mapper.Map<TaskCompletion>(taskCompletionDto);
            var updatedTaskCompletion = await _taskCompletionRepository.Update(taskCompletion);
            if (updatedTaskCompletion is null)
            {
                throw new EntityNotFoundException("No task completion with such id found");
            }
            return _mapper.Map<TaskCompletionDto>(updatedTaskCompletion);
        }

        public async Task DeleteExceeding(long taskId, DateTime endRepeating)
        {
            await _taskCompletionRepository.Delete(taskId, endRepeating);
        }

        public async Task<List<TaskCompletionDto>> AddLacking(List<TaskCompletionDto> completions, DateTime endRepeating)
        {
            var lastTwoCompletions = completions.OrderBy(tc => tc.End).TakeLast(2).ToList();
            var lastCompletion = lastTwoCompletions.Last();
            var repeatingPeriodLength = lastTwoCompletions[1].Start - lastTwoCompletions[1].Start;
            var length = lastCompletion.End - lastCompletion.Start;
            var newCompletions = new List<TaskCompletion>();

            var currentStart = lastCompletion.Start;
            while (currentStart + length <= endRepeating)
            {
                var current = new TaskCompletion
                {
                    Start = currentStart,
                    End = currentStart + length,
                    LastEdited = _dateTimeService.Now(),
                    IsSuccessful = false,
                    TaskId = lastCompletion.TaskId,
                };
                newCompletions.Add(current);
                currentStart += repeatingPeriodLength;
            }
            return _mapper.Map<List<TaskCompletionDto>>(await _taskCompletionRepository.Insert(newCompletions));
        }

        private List<TaskCompletion> Generate(NewTaskDto task, long taskId)
        {
            if (task.RepeatingPeriod == TaskRepeatingPeriod.None)
            {
                return new List<TaskCompletion>
                {
                    new TaskCompletion
                    {
                        Start = task.Start,
                        End = task.End,
                        LastEdited = _dateTimeService.Now(),
                        IsSuccessful = false,
                        TaskId = taskId,
                    }
                };
            }
            var taskCompletions = new List<TaskCompletion>();
            var currentStart = task.Start;
            var length = task.End - task.Start;
            while (currentStart + length <= task.EndRepeating)
            {
                var current = new TaskCompletion
                {
                    Start = currentStart,
                    End = currentStart + length,
                    LastEdited = _dateTimeService.Now(),
                    IsSuccessful = false,
                    TaskId = taskId,
                };
                taskCompletions.Add(current);
                currentStart = GetNextStart(currentStart, task.RepeatingPeriod);
            }
            return taskCompletions;
        }

        private static DateTime GetNextStart(DateTime currentStart, TaskRepeatingPeriod period)
        {
            return period switch
            {
                TaskRepeatingPeriod.Day => currentStart.AddDays(1),
                TaskRepeatingPeriod.Week => currentStart.AddDays(7),
                TaskRepeatingPeriod.Month => currentStart.AddMonths(1),
                TaskRepeatingPeriod.Year => currentStart.AddYears(1),
                _ => throw new InvalidPeriodException()
            };
        }
    }
}

using AutoMapper;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Tasks;
using SlothOrganizer.Services.Abstractions.Utility;

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
            while (currentStart + length < task.EndRepeating)
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

        private DateTime GetNextStart(DateTime currentStart, TaskRepeatingPeriod period)
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

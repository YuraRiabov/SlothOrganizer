using System.Threading.Tasks;
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
        private readonly ITaskCompletionPeriodConverter _taskCompletionPeriodConverter;
        public TaskCompletionService(IMapper mapper, ITaskCompletionRepository taskCompletionRepository, IDateTimeService dateTimeService, ITaskCompletionPeriodConverter taskCompletionPeriodConverter)
        {
            _mapper = mapper;
            _taskCompletionRepository = taskCompletionRepository;
            _dateTimeService = dateTimeService;
            _taskCompletionPeriodConverter = taskCompletionPeriodConverter;
        }

        public async Task<List<TaskCompletionDto>> Create(NewTaskDto newTask, long taskId)
        {
            var taskCompletions = GenerateNew(newTask, taskId);
            return _mapper.Map<List<TaskCompletionDto>>(await _taskCompletionRepository.Insert(taskCompletions));
        }

        public async Task<TaskCompletionDto> Update(TaskCompletionDto taskCompletionDto)
        {
            var taskCompletion = _mapper.Map<TaskCompletion>(taskCompletionDto, options => 
                options.AfterMap((_, tc) => tc.LastEdited = _dateTimeService.Now()));
            var updatedTaskCompletion = await _taskCompletionRepository.Update(taskCompletion);
            if (updatedTaskCompletion is null)
            {
                throw new EntityNotFoundException("No task completion with such id found");
            }
            return _mapper.Map<TaskCompletionDto>(updatedTaskCompletion);
        }

        public async Task Delete(long taskId, DateTime endLimit)
        {
            await _taskCompletionRepository.Delete(taskId, endLimit);
        }

        public async Task Delete(long id)
        {
            await _taskCompletionRepository.Delete(id);
        }

        public async Task<IEnumerable<TaskCompletionDto>> Add(IEnumerable<TaskCompletionDto> completions, DateTime endRepeating)
        {
            var lastTwoCompletions = completions.OrderBy(tc => tc.End).TakeLast(2);
            var lastCompletion = lastTwoCompletions.Last();
            var repeatingPeriodLength = lastCompletion.Start - lastTwoCompletions.First().Start;
            var length = lastCompletion.End - lastCompletion.Start;
            var repeatingPeriod = _taskCompletionPeriodConverter.GetRepeatingPeriod(repeatingPeriodLength);
            var firstStart = GetNextStart(lastCompletion.Start, repeatingPeriod);

            var newCompletions = Generate(firstStart, length, repeatingPeriod, endRepeating, lastCompletion.TaskId);
            return _mapper.Map<IEnumerable<TaskCompletionDto>>(await _taskCompletionRepository.Insert(newCompletions));
        }

        private List<TaskCompletion> GenerateNew(NewTaskDto task, long taskId)
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
            var firstStart = task.Start;
            var length = task.End - task.Start;
            var taskCompletions = Generate(firstStart, length, task.RepeatingPeriod, task.EndRepeating, taskId);
            return taskCompletions;
        }

        private List<TaskCompletion> Generate(DateTime firstStart, TimeSpan length, TaskRepeatingPeriod period, DateTime? endRepeating, long taskId)
        {
            var completions = new List<TaskCompletion>();
            var currentStart = firstStart;
            while (currentStart + length <= endRepeating)
            {
                var current = new TaskCompletion
                {
                    Start = currentStart,
                    End = currentStart + length,
                    LastEdited = _dateTimeService.Now(),
                    IsSuccessful = false,
                    TaskId = taskId,
                };
                completions.Add(current);
                currentStart = GetNextStart(currentStart, period);
            }

            return completions;
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

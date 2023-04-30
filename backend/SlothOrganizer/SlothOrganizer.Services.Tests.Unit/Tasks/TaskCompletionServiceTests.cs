using AutoMapper;
using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Tasks;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Tasks;
using SlothOrganizer.Services.Tasks.Mapping;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit.Tasks
{
    public class TaskCompletionServiceTests
    {
        private readonly TaskCompletionService _taskCompletionService;
        private readonly IMapper _mapper;
        private readonly ITaskCompletionRepository _taskCompletionRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly ITaskCompletionPeriodConverter _taskCompletionPeriodConverter;

        public TaskCompletionServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<TaskMappingProfile>());
            _mapper = config.CreateMapper();
            _taskCompletionRepository = A.Fake<ITaskCompletionRepository>();
            _dateTimeService = A.Fake<IDateTimeService>();
            _taskCompletionPeriodConverter = A.Fake<ITaskCompletionPeriodConverter>();

            _taskCompletionService = new TaskCompletionService(_mapper, _taskCompletionRepository, _dateTimeService, _taskCompletionPeriodConverter);
        }

        [Theory]
        [InlineData(TaskRepeatingPeriod.None, 1, 1)]
        [InlineData(TaskRepeatingPeriod.None, 1000, 1)]
        [InlineData(TaskRepeatingPeriod.Day, 4, 4)]
        [InlineData(TaskRepeatingPeriod.Week, 25, 4)]
        [InlineData(TaskRepeatingPeriod.Month, 64, 3)]
        [InlineData(TaskRepeatingPeriod.Year, 800, 3)]
        public async Task Create(TaskRepeatingPeriod repeatingPeriod, int daysRepeating, int expectedCount)
        {
            var newTask = GetNewTask(repeatingPeriod, daysRepeating);

            A.CallTo(() => _taskCompletionRepository.Insert(A<List<TaskCompletion>>._)).ReturnsLazily((IEnumerable<TaskCompletion> completions) => completions);

            var result = await _taskCompletionService.Create(newTask, 1);

            Assert.Equal(expectedCount, result.Count);
            Assert.True(result.All(c => c.End - c.Start == newTask.End - newTask.Start));
            Assert.Equal(newTask.End, result.First().End);
        }

        [Fact]
        public async Task Update_WhenValidData_ShouldReturn()
        {
            var completionToUpdate = GetTaskCompletion();
            A.CallTo(() => _taskCompletionRepository.Update(A<TaskCompletion>._))
                .ReturnsLazily((TaskCompletion completion) => _mapper.Map<TaskCompletion>(completion));

            var result = await _taskCompletionService.Update(completionToUpdate);

            Assert.Equal(result.Start, completionToUpdate.Start);
            Assert.Equal(result.End, completionToUpdate.End);
            Assert.Equal(result.IsSuccessful, completionToUpdate.IsSuccessful);
        }

        [Fact]
        public async Task Update_WhenInvalidData_ShouldThrow()
        {
            var completionToUpdate = GetTaskCompletion();
            A.CallTo(() => _taskCompletionRepository.Update(A<TaskCompletion>._)).Returns(Task.FromResult<TaskCompletion?>(null));

            var code = async () => await _taskCompletionService.Update(completionToUpdate);

            var exception = await Assert.ThrowsAsync<EntityNotFoundException>(code);
            Assert.Equal("No task completion with such id found", exception.Message);
        }

        [Fact]
        public async Task Delete_WhenDeletingOne_ShouldCallDelete()
        {
            var completionId = 1;
            await _taskCompletionService.Delete(completionId);
            A.CallTo(() => _taskCompletionRepository.Delete(completionId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_WhenDeletingMany_ShouldCallDelete()
        {
            var taskId = 1;
            var endRepeating = new DateTimeOffset();
            await _taskCompletionService.Delete(taskId, endRepeating);
            A.CallTo(() => _taskCompletionRepository.Delete(taskId, endRepeating)).MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineData(1, 100, TaskRepeatingPeriod.Day, 100)]
        [InlineData(7, 100, TaskRepeatingPeriod.Week, 14)]
        [InlineData(30, 100, TaskRepeatingPeriod.Month, 3)]
        [InlineData(366, 700, TaskRepeatingPeriod.Year, 1)]
        public async Task Add_ShouldAddRightAmount(int daysBetweenCompletions, int daysAdded, TaskRepeatingPeriod taskRepeatingPeriod, int expectedCount)
        {
            var taskCompletions = GetTaskCompletions(daysBetweenCompletions);
            var endRepeating = taskCompletions.Max(tc => tc.End).AddDays(daysAdded);
            A.CallTo(() => _taskCompletionRepository.Insert(A<IEnumerable<TaskCompletion>>._))
                .ReturnsLazily((IEnumerable<TaskCompletion> taskCompletions) => taskCompletions);
            A.CallTo(() => _taskCompletionPeriodConverter.GetRepeatingPeriod(A<TimeSpan>._)).Returns(taskRepeatingPeriod);

            var result = await _taskCompletionService.Add(taskCompletions, endRepeating);

            Assert.Equal(expectedCount, result.Count());
            A.CallTo(() => _taskCompletionRepository.Insert(A<List<TaskCompletion>>._)).MustHaveHappenedOnceExactly();
        }

        private static NewTaskDto GetNewTask(TaskRepeatingPeriod period, int days)
        {
            return new NewTaskDto
            {
                DashboardId = 1,
                Title = "Test",
                Description = "Test",
                Start = new DateTime(2023, 1, 1),
                End = new DateTime(2023, 1, 2),
                RepeatingPeriod = period,
                EndRepeating = new DateTime(2023, 1, 1) + TimeSpan.FromDays(days)
            };
        }

        private static TaskCompletionDto GetTaskCompletion()
        {
            return new TaskCompletionDto
            {
                Id = 1,
                TaskId = 1,
                Start = new DateTime(2023, 1, 1),
                End = new DateTime(2023, 1, 2),
                IsSuccessful = true
            };
        }

        private static List<TaskCompletionDto> GetTaskCompletions(int daysBetweenCompletions)
        {
            return new List<TaskCompletionDto>
            {
                new TaskCompletionDto
                {
                    Id = 1,
                    TaskId = 1,
                    Start = new DateTime(2023, 1, 1, 12, 0, 0),
                    End = new DateTime(2023, 1, 1, 13, 0, 0),
                    IsSuccessful = true
                },
                new TaskCompletionDto
                {
                    Id = 2,
                    TaskId = 1,
                    Start = new DateTime(2023, 1, 1, 12, 0, 0).AddDays(daysBetweenCompletions),
                    End = new DateTime(2023, 1, 1, 13, 0, 0).AddDays(daysBetweenCompletions),
                    IsSuccessful = true
                }
            };
        }
    }
}

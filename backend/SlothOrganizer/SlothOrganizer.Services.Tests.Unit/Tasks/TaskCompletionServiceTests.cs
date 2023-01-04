using AutoMapper;
using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Tasks;
using SlothOrganizer.Services.Tasks.Mapping;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SlothOrganizer.Services.Tests.Unit.Tasks
{
    public class TaskCompletionServiceTests
    {
        private readonly TaskCompletionService _taskCompletionService;
        private readonly IMapper _mapper;
        private readonly ITaskCompletionRepository _taskCompletionRepository;
        private readonly IDateTimeService _dateTimeService;

        public TaskCompletionServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<TaskMappingProfile>());
            _mapper = config.CreateMapper();
            _taskCompletionRepository = A.Fake<ITaskCompletionRepository>();
            _dateTimeService= A.Fake<IDateTimeService>();

            _taskCompletionService = new TaskCompletionService(_mapper, _taskCompletionRepository, _dateTimeService);
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

            A.CallTo(() => _taskCompletionRepository.Insert(A<List<TaskCompletion>>._)).ReturnsLazily((List<TaskCompletion> completions) => completions);

            var result = await _taskCompletionService.Create(newTask, 1);

            Assert.Equal(expectedCount, result.Count);
            Assert.True(result.All(c => c.End - c.Start == newTask.End - newTask.Start));
            Assert.Equal(newTask.End, result.First().End);
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
    }
}

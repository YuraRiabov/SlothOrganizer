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
using SlothOrganizer.Services.Utility;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit.Tasks
{
    public class TaskServiceTests
    {
        private readonly TaskService _taskService;
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskCompletionService _taskCompletionService;
        private readonly ITaskCompletionPeriodConverter _taskCompletionPeriodConverter;

        public TaskServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<TaskMappingProfile>());
            _mapper = config.CreateMapper();
            _taskRepository = A.Fake<ITaskRepository>();
            _taskCompletionService = A.Fake<ITaskCompletionService>();
            _taskCompletionPeriodConverter = new TaskCompletionPeriodConverter();

            _taskService = new TaskService(_taskCompletionService, _mapper, _taskRepository, _taskCompletionPeriodConverter);
        }

        [Theory]
        [InlineData(TaskRepeatingPeriod.None)]
        [InlineData(TaskRepeatingPeriod.Month)]
        [InlineData(TaskRepeatingPeriod.Year)]
        public async Task Create_WhenValidPeriod_ShouldCreate(TaskRepeatingPeriod period)
        {
            var newTask = GetNewTask(period);
            var task = _mapper.Map<UserTask>(newTask);
            task.Id = 1;
            var taskCompletions = GetTaskCompletions();
            A.CallTo(() => _taskRepository.Insert(A<UserTask>._)).Returns(task);
            A.CallTo(() => _taskCompletionService.Create(newTask, 1)).Returns(taskCompletions);

            var result = await _taskService.Create(newTask);

            Assert.Equal(taskCompletions, result.TaskCompletions);
            Assert.Equal(newTask.Title, result.Title);
            Assert.Equal(1, result.Id);
        }

        [Theory]
        [InlineData(TaskRepeatingPeriod.Day)]
        [InlineData(TaskRepeatingPeriod.Week)]
        public async Task Create_WhenInalidPeriod_ShouldCreate(TaskRepeatingPeriod period)
        {
            var newTask = GetNewTask(period);

            var code = async () => await _taskService.Create(newTask);

            var exception = await Assert.ThrowsAsync<InvalidPeriodException>(code);
            Assert.Equal("Invalid repeating period", exception.Message);
        }

        [Fact]
        public async Task Get_WhenHasData_ShouldReturn()
        {
            var tasks = GetTasks();
            var dashboardId = 1;
            A.CallTo(() => _taskRepository.Get(dashboardId)).Returns(tasks);

            var result = await _taskRepository.Get(dashboardId);

            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(2, result.Count(t => t.Title == "Test"));
        }

        [Fact]
        public async Task Get_WhenNoData_ShouldReturnEmpty()
        {
            var dashboardId = 1;
            A.CallTo(() => _taskRepository.Get(dashboardId)).Returns(new List<UserTask>());

            var result = await _taskRepository.Get(dashboardId);

            Assert.Empty(result);
        }

        [Fact]
        public async Task Update_WhenNotRepeating_ShouldUpdate()
        {
            var updateTaskDto = GetTaskUpdate();
            updateTaskDto.Task.TaskCompletions = updateTaskDto.Task.TaskCompletions.Take(1).ToList();
            A.CallTo(() => _taskRepository.Update(A<UserTask>._))
                .ReturnsLazily((UserTask task) => task);
            A.CallTo(() => _taskCompletionService.Update(A<TaskCompletionDto>._))
                .Returns(updateTaskDto.TaskCompletion);

            var result = await _taskService.Update(updateTaskDto);

            A.CallTo(() => _taskRepository.Update(A<UserTask>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _taskCompletionService.Update(A<TaskCompletionDto>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _taskCompletionService.Add(A<List<TaskCompletionDto>>._, A<DateTime>._)).MustNotHaveHappened();
            A.CallTo(() => _taskCompletionService.Delete(A<long>._, A<DateTime>._)).MustNotHaveHappened();
            Assert.Equal(result.Id, updateTaskDto.Task.Id);
            Assert.Equal(result.TaskCompletions.Count, updateTaskDto.Task.TaskCompletions.Count);
        }

        [Fact]
        public async Task Update_WhenEndRepeatingLater_ShouldAdd()
        {
            var updateTaskDto = GetTaskUpdate();
            A.CallTo(() => _taskRepository.Update(A<UserTask>._))
                .ReturnsLazily((UserTask task) => task);
            A.CallTo(() => _taskCompletionService.Update(A<TaskCompletionDto>._))
                .Returns(updateTaskDto.TaskCompletion);
            A.CallTo(() => _taskCompletionService.Add(A<List<TaskCompletionDto>>._, A<DateTime>._))
                .Returns(new List<TaskCompletionDto>() { GetTaskCompletion() });

            var result = await _taskService.Update(updateTaskDto);

            A.CallTo(() => _taskRepository.Update(A<UserTask>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _taskCompletionService.Update(A<TaskCompletionDto>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _taskCompletionService.Add(A<List<TaskCompletionDto>>._, A<DateTime>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _taskCompletionService.Delete(A<long>._, A<DateTime>._)).MustNotHaveHappened();
            Assert.Equal(result.Id, updateTaskDto.Task.Id);
            Assert.Equal(result.TaskCompletions.Count, updateTaskDto.Task.TaskCompletions.Count + 1);
        }

        [Fact]
        public async Task Update_WhenEndRepeatingEarlier_ShouldDelete()
        {
            var updateTaskDto = GetTaskUpdate();
            updateTaskDto.EndRepeating = updateTaskDto.EndRepeating.Value.AddDays(-15);
            A.CallTo(() => _taskRepository.Update(A<UserTask>._))
                .ReturnsLazily((UserTask task) => task);
            A.CallTo(() => _taskCompletionService.Update(A<TaskCompletionDto>._))
                .Returns(updateTaskDto.TaskCompletion);

            var result = await _taskService.Update(updateTaskDto);

            A.CallTo(() => _taskRepository.Update(A<UserTask>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _taskCompletionService.Update(A<TaskCompletionDto>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _taskCompletionService.Add(A<List<TaskCompletionDto>>._, A<DateTime>._)).MustNotHaveHappened();
            A.CallTo(() => _taskCompletionService.Delete(A<long>._, A<DateTime>._)).MustHaveHappenedOnceExactly();
            Assert.Equal(result.Id, updateTaskDto.Task.Id);
            Assert.Equal(result.TaskCompletions.Count, updateTaskDto.Task.TaskCompletions.Count - 1);
        }

        [Fact]
        public async Task Update_WhenInvalidData_ShouldThrow()
        {
            var updateTaskDto = GetTaskUpdate();
            A.CallTo(() => _taskRepository.Update(A<UserTask>._)).Returns(Task.FromResult<UserTask?>(null));

            var code = async () => await _taskService.Update(updateTaskDto);

            var exception = await Assert.ThrowsAsync<EntityNotFoundException>(code);
            Assert.Equal("Task with such id not found", exception.Message);
        }

        private static NewTaskDto GetNewTask(TaskRepeatingPeriod period)
        {
            return new NewTaskDto
            {
                DashboardId = 1,
                Title = "Test",
                Description = "Test",
                Start = new DateTime(2023, 1, 1),
                End = new DateTime(2023, 1, 9),
                RepeatingPeriod = period,
                EndRepeating = new DateTime(2024, 1, 1)
            };
        }

        private static TaskCompletionDto GetTaskCompletion()
        {
            return new TaskCompletionDto
            {
                Id = 2,
                TaskId = 1,
                Start = new DateTime(2023, 1, 10),
                End = new DateTime(2023, 1, 19),
                IsSuccessful = false
            };
        }

        private static List<TaskCompletionDto> GetTaskCompletions()
        {
            return new List<TaskCompletionDto>
            {
                new TaskCompletionDto
                {
                    Id = 1,
                    TaskId = 1,
                    Start = new DateTime(2023, 1, 1),
                    End = new DateTime(2023, 1, 9),
                    IsSuccessful = false
                },
                new TaskCompletionDto
                {
                    Id = 2,
                    TaskId = 1,
                    Start = new DateTime(2023, 1, 10),
                    End = new DateTime(2023, 1, 19),
                    IsSuccessful = false
                }
            };
        }

        private static UpdateTaskDto GetTaskUpdate()
        {
            return new UpdateTaskDto
            {
                Task = new TaskDto
                {
                    Id = 1,
                    DashboardId = 1,
                    Title = "Title",
                    Description = "Description",
                    TaskCompletions = GetTaskCompletions(),
                },
                TaskCompletion = GetTaskCompletion(),
                EndRepeating = new DateTime(2023, 2, 1)
            };
        }

        private static List<UserTask> GetTasks()
        {
            return new List<UserTask>
            {
                new UserTask
                {
                    Id = 1,
                    DashboardId = 1,
                    Title = "Test",
                    Description = "Test"
                },
                new UserTask
                {
                    Id = 2,
                    DashboardId = 1,
                    Title = "Test",
                    Description = "Test"
                }
            };
        }
    }
}

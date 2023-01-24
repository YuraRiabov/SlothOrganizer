﻿using AutoMapper;
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
            _taskCompletionService= A.Fake<ITaskCompletionService>();
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
                }
            };
        }
    }
}

﻿using AutoMapper;
using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Tasks.Dashboard;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Tasks;
using SlothOrganizer.Services.Tasks.Mapping;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit.Tasks
{
    public class DashboardServiceTests
    {
        private readonly DashboardService _dashboardService;
        private readonly IMapper _mapper;
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DashboardMappingProfile>());
            _mapper = config.CreateMapper();

            _dashboardRepository = A.Fake<IDashboardRepository>();

            _dashboardService = new DashboardService(_dashboardRepository, _mapper);
        }

        [Fact]
        public async Task Create_ShouldCallRepositoryAndReturnSameData()
        {
            var newDashboard = GetNewDashboard();
            var userId = 1;
            A.CallTo(() => _dashboardRepository.Insert(A<Dashboard>.Ignored)).Returns(
                new Dashboard
                {
                    Title = newDashboard.Title,
                    UserId = userId
                });

            var result = await _dashboardService.Create(newDashboard, userId);

            Assert.Equal(newDashboard.Title, result.Title);
            Assert.Equal(userId, result.UserId);
            A.CallTo(() => _dashboardRepository.Insert(A<Dashboard>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Get_WhenNotEmpty_ShouldReturn()
        {
            var dashboard = GetDashboard();
            A.CallTo(() => _dashboardRepository.Get(dashboard.UserId)).Returns(new List<Dashboard> { dashboard });

            var result = await _dashboardService.Get(dashboard.UserId);

            Assert.NotNull(result);
            Assert.Single(result);
            var firstDashboard = result.First();
            Assert.Equal(dashboard.Title, firstDashboard.Title);
            Assert.Equal(dashboard.Id, firstDashboard.Id);
            Assert.Equal(dashboard.UserId, firstDashboard.UserId);
        }

        [Fact]
        public async Task Get_WhenEmpty_ShouldGenerateDefault()
        {
            var defaultDashboards = GetDefaultDashboards();
            var userId = defaultDashboards[0].UserId;
            A.CallTo(() => _dashboardRepository.Insert(A<Dashboard>._)).ReturnsNextFromSequence(defaultDashboards.ToArray());
            A.CallTo(() => _dashboardRepository.Get(userId)).Returns(new List<Dashboard>());

            var result = await _dashboardService.Get(userId);

            Assert.Equal(2, result.Count);
            Assert.Equal("Routine", result[0].Title);
            Assert.Equal("Work", result[1].Title);
        }

        private static NewDashboardDto GetNewDashboard()
        {
            return new NewDashboardDto
            {
                Title = "Title"
            };
        }

        private static Dashboard GetDashboard()
        {
            return new Dashboard
            {
                Title = "Title",
                UserId = 1,
                Id = 1
            };
        }

        private static List<Dashboard> GetDefaultDashboards()
        {
            return new List<Dashboard>
            {
                new Dashboard
                {
                    Title = "Routine",
                    Id = 1,
                    UserId = 1
                },
                new Dashboard
                {
                    Title = "Work",
                    Id = 2,
                    UserId = 1
                }
            };
        }
    }
}

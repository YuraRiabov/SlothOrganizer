﻿using System.Net;
using SlothOrganizer.Contracts.DTO.Tasks.Dashboard;
using SlothOrganizer.Web.Tests.Integration.Setup.Providers;
using SlothOrganizer.Web.Tests.Integration.Tests.Base;

namespace SlothOrganizer.Web.Tests.Integration.Tests
{
    [UsesVerify]
    [Collection("DbUsingTests")]
    public class DashboardTests : AuthorizedTestBase
    {
        private const string ControllerRoute = "dashboards";
        private readonly DashboardDtoProvider _dashboardDtoProvider;

        public DashboardTests()
        {
            _dashboardDtoProvider = new DashboardDtoProvider();
        }

        [Fact]
        public async Task CreateDashboard_WhenValidData_ShouldCreate()
        {
            await AddAuthorizationHeader();
            var dashboard = _dashboardDtoProvider.GetNewDashboard();

            var response = await Client.PostAsync(ControllerRoute, GetStringContent(dashboard));
            var result = await GetResponse<DashboardDto>(response);

            await Verify(new
            {
                response.StatusCode,
                result
            });
        }

        [Fact]
        public async Task CreateDashboard_WhenEmptyTitle_BadRequest()
        {
            await AddAuthorizationHeader();
            var dashboard = _dashboardDtoProvider.GetNewDashboard();
            dashboard.Title = string.Empty;

            var response = await Client.PostAsync(ControllerRoute, GetStringContent(dashboard));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetDashboards_WhenNoPresent_ShouldCreateDefault()
        {
            await AddAuthorizationHeader();

            var response = await Client.GetAsync($"{ControllerRoute}");
            var result = await GetResponse<List<DashboardDto>>(response);

            await Verify(new
            {
                response.StatusCode,
                result
            });
        }

        [Fact]
        public async Task GetDashboards_WhenExist_ShouldReturn()
        {
            await AddAuthorizationHeader();
            var dashboard = _dashboardDtoProvider.GetNewDashboard();

            await Client.PostAsync(ControllerRoute, GetStringContent(dashboard));

            var response = await Client.GetAsync($"{ControllerRoute}");
            var result = await GetResponse<List<DashboardDto>>(response);

            await Verify(new
            {
                response.StatusCode,
                result
            });
        }
    }
}

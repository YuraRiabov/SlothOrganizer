using System.Net;
using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;
using SlothOrganizer.Web.Tests.Integration.Base;
using SlothOrganizer.Web.Tests.Integration.Setup;

namespace SlothOrganizer.Web.Tests.Integration.Tests
{
    [UsesVerify]
    [Collection("DbUsingTests")]
    public class TasksTests : TestBase
    {
        private const string ControllerRoute = "tasks";

        [Fact]
        public async Task CreateTask_WhenValidData_ShouldCreate()
        {
            await AddAuthorizationHeader();
            A.CallTo(() => DateTimeService.Now()).Returns(new DateTime(2023, 1, 1));

            var dashboard = DtoProvider.GetNewDashboard();
            await Client.PostAsync("dashboards", GetStringContent(dashboard));

            var newTask = DtoProvider.GetNewTask();
            var response = await Client.PostAsync(ControllerRoute, GetStringContent(newTask));
            var result = await GetResponse<TaskDto>(response);

            await Verify(new
            {
                response.StatusCode,
                result
            }).DontScrubDateTimes();
        }

        [Theory]
        [InlineData(TaskRepeatingPeriod.Day, 2, 3)]
        [InlineData(TaskRepeatingPeriod.Month, 30, 40)]
        [InlineData(TaskRepeatingPeriod.None, -1, null)]
        [InlineData(TaskRepeatingPeriod.Week, 2, null)]
        [InlineData(TaskRepeatingPeriod.Week, 2, 1)]
        public async Task CreateTask_WhenInvalidDates_BadRequest(TaskRepeatingPeriod period, int length, int? repeatingLength)
        {
            await AddAuthorizationHeader();
            var newTask = DtoProvider.GetNewTask();
            newTask.End = newTask.Start.AddDays(length);
            newTask.RepeatingPeriod = period;
            newTask.EndRepeating = repeatingLength == null ? null : newTask.Start.AddDays((int)repeatingLength);

            var response = await Client.PostAsync(ControllerRoute, GetStringContent(newTask));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}

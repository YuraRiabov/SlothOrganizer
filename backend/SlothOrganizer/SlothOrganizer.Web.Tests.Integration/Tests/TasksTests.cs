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

            var newTask = DtoProvider.GetNewTask(TaskRepeatingPeriod.Week);
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

        [Fact]
        public async Task Get_WhenHasData_ShouldReturn()
        {
            await AddAuthorizationHeader();

            await SetUpTask();

            var response = await Client.GetAsync($"{ControllerRoute}/1");
            var result = GetResponse<List<TaskDto>>(response);

            await Verify(new
            {
                response.StatusCode,
                result
            }).DontScrubDateTimes();
        }

        [Fact]
        public async Task Update_WhenInvalidTaskId_NotFound()
        {
            await AddAuthorizationHeader();

            var updateTaskDto = DtoProvider.GetUpdateTask();
            var response = await Client.PutAsync(ControllerRoute, GetStringContent(updateTaskDto));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Update_WhenInvalidTaskCompletionId_NotFound()
        {
            await AddAuthorizationHeader();
            await SetUpTask();

            var updateTaskDto = DtoProvider.GetUpdateTask();
            updateTaskDto.TaskCompletion.Id = 0;
            var response = await Client.PutAsync(ControllerRoute, GetStringContent(updateTaskDto));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }

        [Fact]
        public async Task Update_WhenInvalidDates_BadRequest()
        {
            await AddAuthorizationHeader();
            await SetUpTask();

            var updateTaskDto = DtoProvider.GetUpdateTask();
            updateTaskDto.TaskCompletion.End = updateTaskDto.TaskCompletion.Start.AddHours(-1);
            var response = await Client.PutAsync(ControllerRoute, GetStringContent(updateTaskDto));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WhenNotRepeating_ShouldUpdate()
        {
            await AddAuthorizationHeader();
            await SetUpTask(false);

            var updateTaskDto = DtoProvider.GetUpdateTask();
            var response = await Client.PutAsync(ControllerRoute, GetStringContent(updateTaskDto));
            var result = await GetResponse<TaskDto>(response);

            await Verify(new
            {
                response.StatusCode,
                result
            }).DontScrubDateTimes();
        }

        [Fact]
        public async Task Update_WhenRepeatingLater_ShouldAddCompletions()
        {
            await AddAuthorizationHeader();
            await SetUpTask();

            var updateTaskDto = DtoProvider.GetUpdateTask();
            updateTaskDto.EndRepeating = updateTaskDto.EndRepeating.Value.AddDays(40);
            var response = await Client.PutAsync(ControllerRoute, GetStringContent(updateTaskDto));
            var result = await GetResponse<TaskDto>(response);

            await Verify(new
            {
                response.StatusCode,
                result
            }).DontScrubDateTimes();
        }

        [Fact]
        public async Task Update_WhenRepeatingLater_ShouldDeleteCompletions()
        {
            await AddAuthorizationHeader();
            await SetUpTask();

            var updateTaskDto = DtoProvider.GetUpdateTask();
            updateTaskDto.EndRepeating = updateTaskDto.EndRepeating.Value.AddDays(-10);
            var response = await Client.PutAsync(ControllerRoute, GetStringContent(updateTaskDto));
            var result = await GetResponse<TaskDto>(response);

            await Verify(new
            {
                response.StatusCode,
                result
            }).DontScrubDateTimes();
        }
    }
}

using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;
using SlothOrganizer.Web.Tests.Integration.Setup.Providers;

namespace SlothOrganizer.Web.Tests.Integration.Tests.Base
{
    public class TaskTestBase : AuthorizedTestBase
    {
        protected async Task SetUpTask(bool repeating = true)
        {
            var provider = new DashboardDtoProvider();
            A.CallTo(() => DateTimeService.Now()).Returns(new DateTime(2023, 1, 1));
            var dashboard = provider.GetNewDashboard();
            await Client.PostAsync("dashboards", GetStringContent(dashboard));

            var period = repeating ? TaskRepeatingPeriod.Week : TaskRepeatingPeriod.None;
            var newTask = provider.GetNewTask(period);
            await Client.PostAsync("tasks", GetStringContent(newTask));
        }
    }
}

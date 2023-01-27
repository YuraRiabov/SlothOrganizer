using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;
using SlothOrganizer.Web.Tests.Integration.Setup;

namespace SlothOrganizer.Web.Tests.Integration.Tests.Base
{
    public class TaskTestBase : AuthorizedTestBase
    {
        protected async Task SetUpTask(bool repeating = true)
        {
            A.CallTo(() => DateTimeService.Now()).Returns(new DateTime(2023, 1, 1));
            var dashboard = DtoProvider.GetNewDashboard();
            await Client.PostAsync("dashboards", GetStringContent(dashboard));

            var period = repeating ? TaskRepeatingPeriod.Week : TaskRepeatingPeriod.None;
            var newTask = DtoProvider.GetNewTask(period);
            await Client.PostAsync("tasks", GetStringContent(newTask));
        }
    }
}

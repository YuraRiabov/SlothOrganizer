using System.Net;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Persistence.Repositories;
using SlothOrganizer.Web.Tests.Integration.Base;
using SlothOrganizer.Web.Tests.Integration.Setup;

namespace SlothOrganizer.Web.Tests.Integration.Tests
{
    [UsesVerify]
    [Collection("DbUsingTests")]
    public class TaskCompletionTests : TestBase
    {
        private const string ControllerRoute = "task-completions";
        [Fact]
        public async Task Update_WhenInvalidId_NotFound()
        {
            await AddAuthorizationHeader();

            var taskCompletionDto = DtoProvider.GetTaskCompletion();
            var response = await Client.PutAsync(ControllerRoute, GetStringContent(taskCompletionDto));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Update_WhenValid_ShouldUpdate()
        {
            await AddAuthorizationHeader();
            await SetUpTask();

            var taskCompletionDto = DtoProvider.GetTaskCompletion();
            var response = await Client.PutAsync(ControllerRoute, GetStringContent(taskCompletionDto));
            var result = await GetResponse<TaskCompletionDto>(response);

            await Verify(new
            {
                response.StatusCode,
                result
            }).DontScrubDateTimes();
        }

        [Fact]
        public async Task Delete_ShouldDelete()
        {
            await AddAuthorizationHeader();
            await SetUpTask();

            var response = await Client.DeleteAsync($"{ControllerRoute}/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var task = (await new TaskRepository(Context).Get(1)).First(t => t.Id == 1);
            Assert.Empty(task.TaskCompletions.Where(completion => completion.Id == 1));
        }
    }
}

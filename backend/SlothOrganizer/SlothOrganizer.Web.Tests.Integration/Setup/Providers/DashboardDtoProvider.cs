using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.Tasks.Dashboard;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Web.Tests.Integration.Setup.Providers
{
    public class DashboardDtoProvider
    {
        public NewDashboardDto GetNewDashboard()
        {
            return new NewDashboardDto
            {
                Title = "Test",
            };
        }

        public NewTaskDto GetNewTask(TaskRepeatingPeriod period = TaskRepeatingPeriod.None)
        {
            return new NewTaskDto
            {
                DashboardId = 1,
                Title = "Test",
                Description = "Test",
                Start = new DateTime(2023, 1, 1),
                End = new DateTime(2023, 1, 5),
                RepeatingPeriod = period,
                EndRepeating = new DateTime(2023, 2, 5)
            };
        }

        public TaskDto GetTask()
        {
            return new TaskDto
            {
                Id = 1,
                DashboardId = 1,
                Title = "Title",
                Description = "Description",
            };
        }

        public TaskCompletionDto GetTaskCompletion()
        {
            return new TaskCompletionDto
            {
                Id = 1,
                TaskId = 1,
                Start = new DateTime(2023, 1, 1),
                End = new DateTime(2023, 1, 6),
                IsSuccessful = true,
            };
        }

        public UpdateTaskDto GetUpdateTask()
        {
            return new UpdateTaskDto
            {
                Task = GetTask(),
                TaskCompletion = GetTaskCompletion(),
                EndRepeating = new DateTime(2023, 2, 5)
            };
        }
    }
}

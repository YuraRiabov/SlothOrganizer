using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.Tasks.Dashboard;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Web.Tests.Integration.Setup
{
    public static class DtoProvider
    {
        public static VerificationCodeDto GetVerificationCode(string email)
        {
            return new VerificationCodeDto
            {
                VerificationCode = 111111,
                Email = email
            };
        }

        public static NewUserDto GetNewUser()
        {
            return new NewUserDto
            {
                FirstName = "Yura",
                LastName = "Riabov",
                Email = "test@test.com",
                Password = "passw0rd"
            };
        }

        public static LoginDto GetLogin()
        {
            return new LoginDto
            {
                Email = "test@test.com",
                Password = "passw0rd"
            };
        }

        public static ResetPasswordDto GetResetPasswordDto()
        {
            return new ResetPasswordDto
            {
                Email = "test@test.com",
                Password = "newpassw0rd",
                Code = "111111"
            };
        }

        public static NewDashboardDto GetNewDashboard()
        {
            return new NewDashboardDto
            {
                UserId = 1,
                Title = "Test",
            };
        }

        public static NewTaskDto GetNewTask(TaskRepeatingPeriod period = TaskRepeatingPeriod.None)
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

        public static TaskDto GetTask()
        {
            return new TaskDto
            {
                Id = 1,
                DashboardId = 1,
                Title = "Title",
                Description = "Description",
            };
        }

        public static TaskCompletionDto GetTaskCompletion()
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

        public static UpdateTaskDto GetUpdateTask()
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

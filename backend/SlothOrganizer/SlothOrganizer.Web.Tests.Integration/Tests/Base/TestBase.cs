using System.Net.Http.Headers;
using System.Text;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Persistence;
using SlothOrganizer.Services.Abstractions.Email;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Utility;
using SlothOrganizer.Web.Tests.Integration.Setup;

namespace SlothOrganizer.Web.Tests.Integration.Base
{
    public class TestBase : IDisposable
    {
        protected IEmailService EmailSerivce { get; }
        protected IRandomService RandomService { get; }
        protected IRandomService RealRandomService { get; }
        protected IDateTimeService RealDateTimeService { get; } 
        protected IDateTimeService DateTimeService { get; }
        protected ICryptoService CryptoService { get; }
        protected HttpClient Client { get; }
        protected DapperContext Context { get; }
        public TestBase()
        {
            EmailSerivce = A.Fake<IEmailService>();
            A.CallTo(() => EmailSerivce.SendEmail(A<string>._, A<string>._, A<string>._)).Returns(Task.FromResult(1));
            DateTimeService = A.Fake<IDateTimeService>();
            RealDateTimeService = new DateTimeService();
            A.CallTo(() => DateTimeService.Now()).Returns(new DateTime(2022, 12, 8, 12, 0, 0));
            RandomService = A.Fake<IRandomService>();
            RealRandomService = new RandomService();
            A.CallTo(() => RandomService.GetRandomBytes(16)).Returns(RealRandomService.GetRandomBytes(16));
            A.CallTo(() => RandomService.GetRandomNumber(6)).Returns(RealRandomService.GetRandomNumber(6));
            CryptoService = A.Fake<ICryptoService>();
            var factory = new CustomWebApplicationFactory<Startup>(EmailSerivce, RandomService, DateTimeService, CryptoService);
            Client = factory.CreateClient();

            var dapperConfiguration = new Dictionary<string, string>
            {
                { "ConnectionStrings:SqlConnection", "Server=localhost;Database=SlothOrganizerDBTest;Trusted_Connection=True" },
                { "ConnectionStrings:MasterConnection", "Server=localhost;Database=master;Trusted_Connection=True" }
            };
            Context = new DapperContext(new ConfigurationBuilder().AddInMemoryCollection(dapperConfiguration).Build());
        }
        public void Dispose()
        {
            var dbManager = new DatabaseManager(Context);
            dbManager.Drop("SlothOrganizerDBTest");
        }

        protected StringContent GetStringContent<T>(T payload)
        {
            return new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        }

        protected async Task<T> GetResponse<T>(HttpResponseMessage response)
        {
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(contentString);
        }

        protected async Task AddAuthorizationHeader()
        {
            var auth = await SetupVerifiedUser();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.Token.AccessToken);
        }

        protected async Task<UserAuthDto> SetupVerifiedUser()
        {
            var user = await SetupUser();
            var verificationCode = DtoProvider.GetVerificationCode(user.Email);
            var verificationResponse = await Client.PutAsync("auth/verify-email", GetStringContent(verificationCode));
            return await GetResponse<UserAuthDto>(verificationResponse);
        }

        protected async Task<UserDto> SetupUser()
        {
            A.CallTo(() => RandomService.GetRandomNumber(6)).Returns(111111);
            A.CallTo(() => DateTimeService.Now()).Returns(DateTime.Now.AddHours(1));
            var newUser = DtoProvider.GetNewUser();
            var signUpResponse = await Client.PostAsync("auth/sign-up", GetStringContent(newUser));
            return await GetResponse<UserDto>(signUpResponse);
        }

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

using System.Text;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SlothOrganizer.Persistence;
using SlothOrganizer.Services.Abstractions.Email;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Utility;
using SlothOrganizer.Web.Tests.Integration.Setup;

namespace SlothOrganizer.Web.Tests.Integration.Tests.Base
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
    }
}

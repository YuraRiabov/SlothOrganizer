using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Xml.Linq;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using SlothOrganizer.Persistence;
using SlothOrganizer.Services.Abstractions.Email;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Web.Tests.Integration.Setup;

namespace SlothOrganizer.Web.Tests.Integration.Base
{
    public class TestBase : IDisposable
    {
        protected IEmailService EmailSerivce { get; }
        protected IRandomService RandomService { get; }
        protected IDateTimeService DateTimeService { get; }
        protected HttpClient Client { get; }
        protected DapperContext Context { get; }
        public TestBase()
        {
            EmailSerivce = A.Fake<IEmailService>();
            RandomService = A.Fake<IRandomService>();
            DateTimeService = A.Fake<IDateTimeService>();
            var factory = new CustomWebApplicationFactory<Startup>(EmailSerivce, RandomService, DateTimeService);
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
    }
}

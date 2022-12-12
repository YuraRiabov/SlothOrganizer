﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SlothOrganizer.Services.Abstractions.Email;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Web.Tests.Integration.Setup
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly IEmailService _emailService;
        private readonly IRandomService _randomService;
        private readonly IDateTimeService _dateService;
        public CustomWebApplicationFactory(IEmailService emailService, IRandomService randomService, IDateTimeService dateService)
        {
            _emailService = emailService;
            _randomService = randomService;
            _dateService = dateService;
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            builder.ConfigureServices(services =>
            {
                ReplaceService(services, _emailService);
                ReplaceService(services, _randomService);
                ReplaceService(services, _dateService);
            });
        }

        private void ReplaceService<T>(IServiceCollection services, T service) where T : class
        {
            var desriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T));
            services.Remove(desriptor);
            services.AddScoped((_) => service);
        }
    }
}
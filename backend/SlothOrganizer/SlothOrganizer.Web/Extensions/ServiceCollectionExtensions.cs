using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Persistence.Repositories;
using FluentMigrator.Runner;
using SlothOrganizer.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SlothOrganizer.Services.Email.Options;
using SlothOrganizer.Services.Email;
using SlothOrganizer.Services.Auth;
using SlothOrganizer.Services.Utility;
using SlothOrganizer.Services.Abstractions.Auth;
using SlothOrganizer.Services.Abstractions.Email;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Users;
using SlothOrganizer.Services.Abstractions.Users;
using SlothOrganizer.Services.Abstractions.Auth.Tokens;
using SlothOrganizer.Services.Auth.Tokens;
using SlothOrganizer.Services.Auth.Tokens.Options;
using SlothOrganizer.Services.Auth.UserVerification;
using SlothOrganizer.Services.Abstractions.Auth.UserVerification;
using SlothOrganizer.Services.Tasks;
using SlothOrganizer.Services.Abstractions.Tasks;

namespace SlothOrganizer.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskCompletionRepository, TaskCompletionRepository>();
            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserCredentialsService, UserCredentialsService>();
            services.AddScoped<IAccessTokenService, AccessTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IRandomService, RandomService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IVerificationCodeService, VerificationCodeService>();
            services.AddScoped<INotificationService, EmailNotificationService>();
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskCompletionService, TaskCompletionService>();
            return services;
        }

        public static IServiceCollection AddJwtOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSection = configuration.GetRequiredSection("Jwt");
            services.Configure<JwtOptions>(options =>
            {
                jwtSection.Bind(options);
                options.Secret = configuration["JwtKey"];
            });
            return services;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            var smtpSection = configuration.GetRequiredSection("Smtp");
            services.Configure<SmtpOptions>(options =>
            {
                smtpSection.Bind(options);
                options.Password = configuration["SmtpPassword"];
            });
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }

        public static IServiceCollection AddFluentMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddSqlServer()
                    .WithGlobalConnectionString(configuration.GetConnectionString("SqlConnection"))
                    .ScanIn(typeof(DapperContext).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());
            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = configuration["JwtKey"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }
    }
}

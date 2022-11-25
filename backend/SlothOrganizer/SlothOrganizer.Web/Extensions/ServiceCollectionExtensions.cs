using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Persistence.Repositories;
using SlothOrganizer.Services.Abstractions;
using SlothOrganizer.Services;
using FluentMigrator.Runner;
using SlothOrganizer.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SlothOrganizer.Services.Options;

namespace SlothOrganizer.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();
            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IVerificationCodeService, VerificationCodeService>();
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]))
                };
            });
            return services;
        }
    }
}

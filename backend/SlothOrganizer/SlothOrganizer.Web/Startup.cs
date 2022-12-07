using FluentValidation.AspNetCore;
using SlothOrganizer.Persistence;
using SlothOrganizer.Services.Users;
using SlothOrganizer.Web.Extensions;
using SlothOrganizer.Web.Middleware;
using SlothOrganizer.Web.Middleware.Validation.Users;

namespace SlothOrganizer.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddSingleton<DatabaseManager>();

            services.AddRepositories();
            services.AddCustomServices();
            services.AddEmailService(Configuration);
            services.AddJwtOptions(Configuration);
            services.AddFluentMigrations(Configuration);
            services.AddCustomAuthentication(Configuration);


            services.AddAutoMapper(typeof(UserMappingProfile).Assembly);

            services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining(typeof(NewUserDtoValidator)));

            services.AddCors();

            services.AddControllers()
                .AddApplicationPart(typeof(SlothOrganizer.Presentation.AssemblyReference).Assembly);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.MigrateDatabase();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options =>
                options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

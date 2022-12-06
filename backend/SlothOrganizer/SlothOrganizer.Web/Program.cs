using FluentValidation.AspNetCore;
using SlothOrganizer.Persistence;
using SlothOrganizer.Services.Users;
using SlothOrganizer.Web.Extensions;
using SlothOrganizer.Web.Middleware;
using SlothOrganizer.Web.Middleware.Validation.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<DatabaseManager>();

builder.Services.AddRepositories();
builder.Services.AddCustomServices();
builder.Services.AddEmailService(builder.Configuration);
builder.Services.AddJwtOptions(builder.Configuration);
builder.Services.AddFluentMigrations(builder.Configuration);
builder.Services.AddCustomAuthentication(builder.Configuration);


builder.Services.AddAutoMapper(typeof(UserMappingProfile).Assembly);

builder.Services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining(typeof(NewUserDtoValidator)));

builder.Services.AddCors();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(SlothOrganizer.Presentation.AssemblyReference).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MigrateDatabase();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

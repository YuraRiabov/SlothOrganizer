using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Persistence;
using SlothOrganizer.Persistence.Repositories;
using SlothOrganizer.Services;
using SlothOrganizer.Services.Abstractions;
using SlothOrganizer.Services.MappingProfiles;
using SlothOrganizer.Web.Extensions;
using SlothOrganizer.Web.Middleware;
using SlothOrganizer.Web.Middleware.Validation.Users;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<DatabaseManager>();

builder.Services.AddRepositories();
builder.Services.AddCustomServices();
builder.Services.AddFluentMigrations(builder.Configuration);
builder.Services.AddCustomAuthentication(builder.Configuration);


builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);

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

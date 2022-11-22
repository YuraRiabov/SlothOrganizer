using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Persistence;
using SlothOrganizer.Persistence.Repositories;
using SlothOrganizer.Services;
using SlothOrganizer.Services.Abstractions;
using SlothOrganizer.Services.MappingProfiles;
using SlothOrganizer.Web.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<DatabaseManager>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(c => c.AddSqlServer()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("SqlConnection"))
        .ScanIn(typeof(DapperContext).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(SlothOrganizer.Presentation.AssemblyReference).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

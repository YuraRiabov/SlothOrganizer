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
using SlothOrganizer.Web.Middleware;
using SlothOrganizer.Web.Middleware.Validation.Users;
using System.Reflection;
using System.Text;

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

builder.Services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining(typeof(NewUserDtoValidator)));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]))
    };
});

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

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

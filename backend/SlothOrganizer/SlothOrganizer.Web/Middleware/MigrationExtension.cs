using FluentMigrator.Runner;
using SlothOrganizer.Persistence;

namespace SlothOrganizer.Web.Middleware
{
    public static class MigrationExtension
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var databaseService = scope.ServiceProvider.GetRequiredService<DatabaseManager>();
            var migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var dbName = configuration["DatabaseName"];

            databaseService.CreateIfAbsent(dbName);
            migrationRunner.ListMigrations();
            migrationRunner.MigrateUp();
            return app;
        }
    }
}

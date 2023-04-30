using FluentMigrator;

namespace SlothOrganizer.Persistence.Migrations
{
    [Migration(202212270002)]
    public class AddTaskAndTaskCompletion_202212270002 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey()
                .FromTable("TaskCompletions").ForeignColumn("TaskId")
                .ToTable("Tasks").PrimaryColumn("Id");
            Delete.Table("TaskCompletions");
            Delete.ForeignKey()
                .FromTable("Tasks").ForeignColumn("DashboardId")
                .ToTable("Dashboards").PrimaryColumn("Id");
            Delete.Table("Tasks");
        }

        public override void Up()
        {
            Create.Table("Tasks")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("DashboardId").AsInt64().NotNullable()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Description").AsString().NotNullable();

            Create.ForeignKey()
                .FromTable("Tasks").ForeignColumn("DashboardId")
                .ToTable("Dashboards").PrimaryColumn("Id");

            Create.Table("TaskCompletions")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("TaskId").AsInt64().NotNullable()
                .WithColumn("Start").AsDateTime().NotNullable()
                .WithColumn("End").AsDateTime().NotNullable()
                .WithColumn("LastEdited").AsDateTime();

            Create.ForeignKey()
                .FromTable("TaskCompletions").ForeignColumn("TaskId")
                .ToTable("Tasks").PrimaryColumn("Id");
        }
    }
}

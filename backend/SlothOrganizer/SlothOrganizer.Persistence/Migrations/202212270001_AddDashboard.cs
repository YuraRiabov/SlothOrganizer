using FluentMigrator;

namespace SlothOrganizer.Persistence.Migrations
{
    [Migration(202212270001)]
    public class AddDashboard_202212270001 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey()
                .FromTable("Dashboards").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");
            Delete.Table("Dashboards");
        }

        public override void Up()
        {
            Create.Table("Dashboards")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("UserId").AsInt64().NotNullable()
                .WithColumn("Title").AsString().NotNullable();

            Create.ForeignKey()
                .FromTable("Dashboards").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");
        }
    }
}

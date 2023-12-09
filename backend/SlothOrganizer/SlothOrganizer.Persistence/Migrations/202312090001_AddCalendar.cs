using FluentMigrator;

namespace SlothOrganizer.Persistence.Migrations;

[Migration(202312090001)]
public class AddCalendar_202312090001 : Migration
{
    public override void Up()
    {
        Create.Table("Calendars")
            .WithColumn("Id").AsInt64().Identity().PrimaryKey()
            .WithColumn("UserId").AsInt64().NotNullable()
            .WithColumn("ConnectedCalendar").AsString(50).NotNullable()
            .WithColumn("RefreshToken").AsString(255).NotNullable()
            .WithColumn("Uid").AsString(100).NotNullable();
        
        Create.ForeignKey()
            .FromTable("Calendars").ForeignColumn("UserId")
            .ToTable("Users").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.ForeignKey()
            .FromTable("Calendars").ForeignColumn("UserId")
            .ToTable("Users").PrimaryColumn("Id");
        
        Delete.Table("Calendars");
    }
}
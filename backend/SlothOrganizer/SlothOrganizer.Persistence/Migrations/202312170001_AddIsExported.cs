using FluentMigrator;

namespace SlothOrganizer.Persistence.Migrations;

[Migration(202312170001)]
public class AddIsExported_202312170001 : Migration
{
    public override void Down()
    {
        Delete.Column("IsExported").FromTable("TaskCompletions");
    }

    public override void Up()
    {
        Create.Column("IsExported").OnTable("TaskCompletions").AsBoolean().NotNullable().WithDefaultValue(false);
    }
}
using FluentMigrator;

namespace SlothOrganizer.Persistence.Migrations
{
    [Migration(202301030001)]
    public class AddTaskCompletionIsSuccessful_202301030001 : Migration
    {
        public override void Down()
        {
            Delete.Column("IsSuccessful").FromTable("TaskCompletions");
        }

        public override void Up()
        {
            Create.Column("IsSuccessful").OnTable("TaskCompletions").AsBoolean().NotNullable();
        }
    }
}

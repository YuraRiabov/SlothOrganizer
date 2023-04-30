using FluentMigrator;

namespace SlothOrganizer.Persistence.Migrations
{
    [Migration(202301170001)]
    public class AddUserAvatar_202301170001 : Migration
    {
        public override void Down()
        {
            Delete.Column("AvatarUrl")
                .FromTable("Users");
        }

        public override void Up()
        {
            Create.Column("AvatarUrl")
                .OnTable("Users")
                .AsString()
                .Nullable();
        }
    }
}

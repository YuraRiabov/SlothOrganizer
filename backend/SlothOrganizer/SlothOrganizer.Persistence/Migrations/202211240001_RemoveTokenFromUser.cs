using FluentMigrator;

namespace SlothOrganizer.Persistence.Migrations
{
    [Migration(202211240001)]
    public class RemoveTokenFromUser_202211240001 : Migration
    {
        public override void Down()
        {
            Alter.Table("Users")
                .AddColumn("RefreshToken")
                .AsString().Nullable();
        }

        public override void Up()
        {
            Delete.Column("RefreshToken").FromTable("Users");
        }
    }
}

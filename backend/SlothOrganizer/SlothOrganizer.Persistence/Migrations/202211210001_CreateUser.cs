using FluentMigrator;

namespace SlothOrganizer.Persistence.Migrations
{
    [Migration(202211210001)]
    public class CreateUser_202211210001 : Migration
    {
        public override void Down()
        {
            Delete.Table("Users");
        }

        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt64().NotNullable().Identity().PrimaryKey()
                .WithColumn("FirstName").AsString(30).NotNullable()
                .WithColumn("LastName").AsString(30).NotNullable()
                .WithColumn("Email").AsString(50).NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("Salt").AsString().NotNullable()
                .WithColumn("EmailVerified").AsBoolean().NotNullable()
                .WithColumn("RefreshToken").AsString().Nullable();
        }
    }
}

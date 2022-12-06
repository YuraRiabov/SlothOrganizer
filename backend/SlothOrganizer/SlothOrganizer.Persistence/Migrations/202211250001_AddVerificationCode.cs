using FluentMigrator;

namespace SlothOrganizer.Persistence.Migrations
{
    [Migration(202211250001)]
    public class AddVerificationCode_202211250001 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey()
                .FromTable("VerificationCodes").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");
            Delete.Table("VerificationCodes");
        }

        public override void Up()
        {
            Create.Table("VerificationCodes")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("UserId").AsInt64().NotNullable()
                .WithColumn("Code").AsInt32().NotNullable()
                .WithColumn("ExpirationTime").AsDateTime().NotNullable();

            Create.ForeignKey()
                .FromTable("VerificationCodes").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");
        }
    }
}

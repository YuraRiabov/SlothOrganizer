using FluentMigrator;

namespace SlothOrganizer.Persistence.Migrations
{
    [Migration(202301250001)]
    public class TransferToOffset_202301250001 : Migration
    {
        public override void Down()
        {
            Alter.Column("Start").OnTable("TaskCompletions").AsDateTime();
            Alter.Column("End").OnTable("TaskCompletions").AsDateTime();
            Alter.Column("LastEdited").OnTable("TaskCompletions").AsDateTime().Nullable();
            Alter.Column("ExpirationTime").OnTable("RefreshTokens").AsDateTime();
            Alter.Column("ExpirationTime").OnTable("VerificationCodes").AsDateTime();
        }

        public override void Up()
        {
            Alter.Column("Start").OnTable("TaskCompletions").AsDateTimeOffset();
            Alter.Column("End").OnTable("TaskCompletions").AsDateTimeOffset();
            Alter.Column("LastEdited").OnTable("TaskCompletions").AsDateTimeOffset().Nullable();
            Alter.Column("ExpirationTime").OnTable("RefreshTokens").AsDateTimeOffset();
            Alter.Column("ExpirationTime").OnTable("VerificationCodes").AsDateTimeOffset();
        }
    }
}

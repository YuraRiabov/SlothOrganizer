using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace SlothOrganizer.Persistence.Migrations
{
    [Migration(20221201)]
    public class AddRefreshTokens_20221201 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey()
                .FromTable("RefreshTokens").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");
            Delete.Table("RefreshTokens");
        }

        public override void Up()
        {
            Create.Table("RefreshTokens")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("UserId").AsInt64().NotNullable()
                .WithColumn("Token").AsString().NotNullable()
                .WithColumn("ExpirationTime").AsDateTime().NotNullable();

            Create.ForeignKey()
                .FromTable("RefreshTokens").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");
        }
    }
}

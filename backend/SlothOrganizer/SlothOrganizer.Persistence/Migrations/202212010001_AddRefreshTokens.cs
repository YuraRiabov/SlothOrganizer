﻿using FluentMigrator;

namespace SlothOrganizer.Persistence.Migrations
{
    [Migration(202212010001)]
    public class AddRefreshTokens_202212010001 : Migration
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
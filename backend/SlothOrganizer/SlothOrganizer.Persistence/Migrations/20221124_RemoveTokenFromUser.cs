using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace SlothOrganizer.Persistence.Migrations
{
    [Migration(20221124)]
    public class RemoveTokenFromUser_20221124 : Migration
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

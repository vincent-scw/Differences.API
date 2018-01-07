using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Differences.DataAccess.Migrations
{
    public partial class AddMicrosoftIdToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MicrosoftId",
                table: "Users",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex("IX_USERS_MicrosoftId", "Users", "MicrosoftId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex("IX_USERS_MicrosoftId", "Users");

            migrationBuilder.DropColumn(
                name: "MicrosoftId",
                table: "Users");
        }
    }
}

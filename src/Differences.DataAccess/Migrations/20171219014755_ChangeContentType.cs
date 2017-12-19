using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Differences.DataAccess.Migrations
{
    public partial class ChangeContentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Answers",
                type: "ntext",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 4000);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Answers",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "ntext");
        }
    }
}

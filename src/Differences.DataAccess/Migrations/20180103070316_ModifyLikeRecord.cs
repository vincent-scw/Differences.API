using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Differences.DataAccess.Migrations
{
    public partial class ModifyLikeRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "LikeRecords",
                newName: "QuestionId");

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "LikeRecords",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "LikeRecords");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "LikeRecords",
                newName: "SubjectId");
        }
    }
}

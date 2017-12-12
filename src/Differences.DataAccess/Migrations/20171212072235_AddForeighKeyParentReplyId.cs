using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Differences.DataAccess.Migrations
{
    public partial class AddForeighKeyParentReplyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Answers_ParentReplyId",
                table: "Answers",
                column: "ParentReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Answers_ParentReplyId",
                table: "Answers",
                column: "ParentReplyId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Answers_ParentReplyId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_ParentReplyId",
                table: "Answers");
        }
    }
}

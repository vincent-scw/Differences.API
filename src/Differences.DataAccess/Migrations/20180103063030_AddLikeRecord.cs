using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Differences.DataAccess.Migrations
{
    public partial class AddLikeRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReputationLog_Users_UserId",
                table: "UserReputationLog");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReputationLog_UserScores_UserId",
                table: "UserReputationLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReputationLog",
                table: "UserReputationLog");

            migrationBuilder.RenameTable(
                name: "UserReputationLog",
                newName: "UserReputationLogs");

            migrationBuilder.RenameIndex(
                name: "IX_UserReputationLog_UserId",
                table: "UserReputationLogs",
                newName: "IX_UserReputationLogs_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReputationLogs",
                table: "UserReputationLogs",
                column: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_UserContributionLogs_Users_UserId",
                table: "UserContributionLogs"
                );

            migrationBuilder.AddForeignKey(
                name: "FK_UserContributionLogs_Users_UserId",
                table: "UserContributionLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.CreateTable(
                name: "LikeRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    SubjectId = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeRecords_UserId_SubjectId",
                table: "LikeRecords",
                columns: new string[] { "UserId", "SubjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserReputationLogs_Users_UserId",
                table: "UserReputationLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReputationLogs_UserScores_UserId",
                table: "UserReputationLogs",
                column: "UserId",
                principalTable: "UserScores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReputationLogs_Users_UserId",
                table: "UserReputationLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReputationLogs_UserScores_UserId",
                table: "UserReputationLogs");

            migrationBuilder.DropTable(
                name: "LikeRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReputationLogs",
                table: "UserReputationLogs");

            migrationBuilder.RenameTable(
                name: "UserReputationLogs",
                newName: "UserReputationLog");

            migrationBuilder.RenameIndex(
                name: "IX_UserReputationLogs_UserId",
                table: "UserReputationLog",
                newName: "IX_UserReputationLog_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReputationLog",
                table: "UserReputationLog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReputationLog_Users_UserId",
                table: "UserReputationLog",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReputationLog_UserScores_UserId",
                table: "UserReputationLog",
                column: "UserId",
                principalTable: "UserScores",
                principalColumn: "Id");
        }
    }
}

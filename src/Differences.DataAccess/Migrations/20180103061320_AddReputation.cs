using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Differences.DataAccess.Migrations
{
    public partial class AddReputation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReputationValue",
                table: "UserScores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserReputationLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: true),
                    LastUpdatedBy = table.Column<Guid>(nullable: true),
                    ReputationTypeId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReputationLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReputationLog_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserReputationLog_UserScores_UserId",
                        column: x => x.UserId,
                        principalTable: "UserScores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserReputationLog_UserId",
                table: "UserReputationLog",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserReputationLog");

            migrationBuilder.DropColumn(
                name: "ReputationValue",
                table: "UserScores");
        }
    }
}

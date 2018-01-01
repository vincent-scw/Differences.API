using Microsoft.EntityFrameworkCore.Migrations;

namespace Differences.DataAccess.Migrations
{
    public partial class InitializeValueForContribution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO dbo.UserScores
                                        ( Id ,
                                          ContributeValue ,
                                          CreateTime ,
                                          CreatedBy ,
                                          LastUpdateTime ,
                                          LastUpdatedBy
                                        )
                                SELECT Id, 0, CreateTime, CreatedBy, NULL, NULL
                                FROM dbo.Users 

                                INSERT INTO dbo.UserContributionLogs
                                        ( ContributeTypeId ,
                                          CreateTime ,
                                          CreatedBy ,
                                          LastUpdateTime ,
                                          LastUpdatedBy ,
                                          SubjectId ,
                                          UserId ,
                                          Value
                                        )
                                SELECT 1, CreateTime, '00000000-0000-0000-0000-000000000000', NULL, NULL, Id, OwnerId, 1
                                FROM dbo.Questions

                                INSERT INTO dbo.UserContributionLogs
                                        ( ContributeTypeId ,
                                          CreateTime ,
                                          CreatedBy ,
                                          LastUpdateTime ,
                                          LastUpdatedBy ,
                                          SubjectId ,
                                          UserId ,
                                          Value
                                        )
                                SELECT 2, CreateTime, '00000000-0000-0000-0000-000000000000', NULL, NULL, Id, OwnerId, 1
                                FROM dbo.Answers

                                UPDATE dbo.UserScores SET ContributeValue=
                                ((SELECT COUNT(dbo.Questions.Id)
                                FROM dbo.Users 
                                LEFT JOIN dbo.Questions ON Questions.OwnerId = Users.Id
                                WHERE dbo.UserScores.Id = dbo.Users.Id)
                                + (SELECT COUNT(dbo.Answers.Id)
                                FROM dbo.Users 
                                LEFT JOIN dbo.Answers ON Answers.OwnerId = Users.Id
                                WHERE dbo.UserScores.Id = dbo.Users.Id))", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

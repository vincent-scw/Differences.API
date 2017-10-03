using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Api.Model;
using Differences.Domain.Users;
using Differences.Interaction.Models;
using GraphQL.Types;
using Differences.Interaction.Repositories;
using Differences.Domain.Questions;

namespace Differences.Api.Queries
{
    public class DifferencesQuery : ObjectGraphType<object>
    {
        public DifferencesQuery(
            IUserService userService,
            IQuestionService questionService)
        {
            Field<ListGraphType<UserType>>(
                "topUsers",
                resolve: context => Task.FromResult(userService.GetTopReputationUsers(1)));

            Field<UserType>(
                "checkUserInDb",
                resolve: context =>
                {
                    var user = ((GraphQLUserContext) context.UserContext).UserInfo;
                    return Task.FromResult(userService.FindOrCreate(user.GlobalId, user.DisplayName, user.Email, user.AvatarUrl));
                });

            Field<QuestionType>(
                "questions",
                resolve: context =>
                {
                    return questionService.GetQuestionsByCategory(1);
                });
        }
    }
}

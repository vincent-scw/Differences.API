using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Api.Model;
using Differences.Domain.Users;
using Differences.Interaction.Models;
using GraphQL.Types;
using Differences.Interaction.Repositories;

namespace Differences.Api.Queries
{
    public partial class DifferencesQuery : GraphQLTypeBase<object>
    {
        public DifferencesQuery(
            IArticleRepository articleRepository, 
            IQuestionRepository questionRepository, 
            IReplyRepository replyRepository,
            IUserService userService)
            : base(articleRepository, questionRepository, replyRepository)
        {

            RegisterArticles();

            Field<ListGraphType<UserType>>(
                "topUsers",
                resolve: context => Task.FromResult(userService.GetTopReputationUsers(1).ToList()));

            Field<UserType>(
                "checkUserInDb",
                resolve: context =>
                {
                    var user = ((GraphQLUserContext) context.UserContext).UserInfo;
                    return Task.FromResult(userService.FindOrCreate(user.GlobalId, user.DisplayName, user.Email, user.AvatarUrl));
                });
        }
    }
}

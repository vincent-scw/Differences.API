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
                    var user = ((GraphQLUserContext) context.UserContext).User;

                    var userGuidString = user.FindFirst(x => x.Type.Equals("aud")).Value;
                    Guid.TryParse(userGuidString, out Guid userGuid);
                    var displayName = user.FindFirst(x => x.Type.Equals("name")).Value;
                    var email = user.FindFirst(x => x.Type.Equals("emails")).Value;
                    //var avatarUrl = user.FindFirst(x => x.Type.Equals("avatarUrl")).Value;

                    return Task.FromResult(userService.FindOrCreate(userGuid, displayName, email, null));
                });
        }
    }
}

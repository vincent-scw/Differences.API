using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Api.Model;
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
            IReplyRepository replyRepository)
            : base(articleRepository, questionRepository, replyRepository)
        {
            RegisterArticles();

            Field<ListGraphType<UserType>>(
                "mvp_users",
                resolve: context => Task.FromResult(new List<User>
                {
                    new User {Id = 12345, Name = "test", DisplaykName = "vincent shen"}
                }));
        }
    }
}

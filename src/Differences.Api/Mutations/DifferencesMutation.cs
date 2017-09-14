using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Interaction.Repositories;
using GraphQL.Types;

namespace Differences.Api.Mutations
{
    public partial class DifferencesMutation : GraphQLTypeBase<object>
    {
        public DifferencesMutation(
            IArticleRepository articleRepository, 
            IQuestionRepository questionRepository, 
            IReplyRepository replyRepository)
            : base(articleRepository, questionRepository, replyRepository)
        {
            Name = "Mutation";
        }
    }
}

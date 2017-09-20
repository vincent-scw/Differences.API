using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Api.Model;
using Differences.Interaction.Models;
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
            Name = "DifferencesMutation";

            Field<QuestionType>(
                "submitQuestion",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<QuestionInputType>> { Name = "question"}
                ),
                resolve: context =>
                {
                    var question = context.GetArgument<Question>("question");
                    return questionRepository.Add(question);
                }
            );
        }
    }
}

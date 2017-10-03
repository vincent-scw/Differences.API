using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Api.Model;
using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using GraphQL.Types;
using Differences.Domain.Questions;

namespace Differences.Api.Mutations
{
    public partial class DifferencesMutation : ObjectGraphType<object>
    {
        public DifferencesMutation(
            IQuestionService questionService)
        {
            Name = "DifferencesMutation";

            Field<QuestionType>(
                "submitQuestion",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<QuestionInputType>> { Name = "question"}
                ),
                resolve: context =>
                {
                    var user = ((GraphQLUserContext)context.UserContext).UserInfo;
                    var question = context.GetArgument<Question>("question");
                    return questionService.AskQuestion(question.Title, question.Content, user.GlobalId);
                }
            );
        }
    }
}

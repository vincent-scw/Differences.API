using Differences.Api.Model;
using Differences.Domain.Questions;
using Differences.Interaction.DataTransferModels;
using GraphQL.Types;

namespace Differences.Api.Mutations
{
    public class DifferencesMutation : ObjectGraphType<object>
    {
        public DifferencesMutation(
            IQuestionService questionService)
        {
            Name = "DifferencesMutation";

            #region Question
            FieldAsync<QuestionType>(
                "submitQuestion",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SubjectInputType>> { Name = "question"}
                ),
                resolve: context =>
                {
                    var user = ((GraphQLUserContext)context.UserContext).UserInfo;
                    var question = context.GetArgument<SubjectModel>("question");
                    return question.Id == 0
                        ? questionService.AskQuestion(question, user.Id)
                        : questionService.UpdateQuestion(question, user.Id);
                }
            );

            FieldAsync<AnswerType>(
                "submitAnswer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ReplyInputType>> { Name = "answer" }
                ),
                resolve: context =>
                {
                    var user = ((GraphQLUserContext)context.UserContext).UserInfo;
                    var answer = context.GetArgument<ReplyModel>("answer");
                    return answer.Id == 0 
                        ? questionService.AddAnswer(answer, user.Id)
                        : questionService.UpdateAnswer(answer, user.Id);
                }
            );
            #endregion
        }
    }
}

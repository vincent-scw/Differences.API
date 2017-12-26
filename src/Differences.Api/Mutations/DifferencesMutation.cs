using Differences.Api.Extensions;
using Differences.Api.Model;
using Differences.Common;
using Differences.Domain.Questions;
using Differences.Domain.Users;
using Differences.Interaction.DataTransferModels;
using GraphQL.Types;

namespace Differences.Api.Mutations
{
    public class DifferencesMutation : ObjectGraphType<object>
    {
        public DifferencesMutation(
            IUserService userService,
            IQuestionService questionService)
        {
            Name = "DifferencesMutation";

            FieldAsync<UserType>(
                "checkUserInDb",
                resolve: context =>
                {
                    var user = ((GraphQLUserContext)context.UserContext).UserInfo;
                    return userService.FindOrCreate(user.Id, user.DisplayName, user.Email, user.AvatarUrl);
                });

            #region Question
            FieldAsync<QuestionType>(
                "submitQuestion",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SubjectInputType>> { Name = "question"}
                ),
                resolve: context =>
                {
                    try
                    {
                        var user = ((GraphQLUserContext) context.UserContext).UserInfo;
                        var question = context.GetArgument<SubjectModel>("question");
                        return question.Id == 0
                            ? questionService.AskQuestion(question, user.Id)
                            : questionService.UpdateQuestion(question, user.Id);
                    }
                    catch (DefinedException ex)
                    {
                        context.Errors.Add(ex);
                        return null;
                    }
                }
            );

            FieldAsync<AnswerType>(
                "submitAnswer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ReplyInputType>> { Name = "answer" }
                ),
                resolve: context =>
                {
                    try
                    {
                        var user = ((GraphQLUserContext) context.UserContext).UserInfo;
                        var answer = context.GetArgument<ReplyModel>("answer");
                        return answer.Id == 0
                            ? questionService.AddAnswer(answer, user.Id)
                            : questionService.UpdateAnswer(answer, user.Id);
                    }
                    catch (DefinedException ex)
                    {
                        context.Errors.Add(ex);
                        return null;
                    }
                }
            );
            #endregion
        }
    }
}

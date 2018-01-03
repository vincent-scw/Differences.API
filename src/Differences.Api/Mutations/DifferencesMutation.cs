using Differences.Api.Extensions;
using Differences.Api.Model;
using Differences.Common;
using Differences.Domain.LikeRecords;
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
            IQuestionService questionService,
            ILikeRecordService likeRecordService)
        {
            Name = "DifferencesMutation";

            #region User
            FieldAsync<UserType>(
                "checkUserInDb",
                resolve: context => userService.FindOrCreate());

            FieldAsync<UserType>(
                "updateUserInfo",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputType>> {Name = "user"}),
                resolve: context =>
                {
                    var newUser = context.GetArgument<UserModel>("user");
                    return userService.UpdateUser(newUser);
                });
            #endregion

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
                        var question = context.GetArgument<SubjectModel>("question");
                        return question.Id == 0
                            ? questionService.AskQuestion(question)
                            : questionService.UpdateQuestion(question);
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
                        var answer = context.GetArgument<ReplyModel>("answer");
                        return answer.Id == 0
                            ? questionService.AddAnswer(answer)
                            : questionService.UpdateAnswer(answer);
                    }
                    catch (DefinedException ex)
                    {
                        context.Errors.Add(ex);
                        return null;
                    }
                }
            );

            FieldAsync<UserType>(
                "likeAnswer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LikeInputType>> {Name = "likeRecord"}
                ),
                resolve:
                context =>
                {
                    try
                    {
                        var likeRecord = context.GetArgument<LikeRecordModel>("likeRecord");
                        return likeRecordService.AddRecord(likeRecord);
                    }
                    catch (DefinedException ex)
                    {
                        context.Errors.Add(ex);
                        return null;
                    }
                });

            #endregion
        }
    }
}

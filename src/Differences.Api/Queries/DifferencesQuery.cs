using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Api.Model;
using Differences.Domain.Users;
using Differences.Interaction.EntityModels;
using GraphQL.Types;
using Differences.Interaction.Repositories;
using Differences.Domain.Questions;
using Differences.Interaction.DataTransferModels;
using Differences.Domain;
using Differences.Domain.LikeRecords;

namespace Differences.Api.Queries
{
    public class DifferencesQuery : ObjectGraphType<object>
    {
        public DifferencesQuery(
            IUserService userService,
            IQuestionService questionService,
            ILikeRecordService likeRecordService,
            IAccountService accountService)
        {
            Name = "DifferencesQuery";

            #region User
            FieldAsync<ListGraphType<UserType>>(
                "topUsers",
                resolve: context => userService.GetTopReputationUsers(1));

            FieldAsync<AuthResponseType>(
                "auth",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "type"},
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "code"}),
                resolve: context =>
                {
                    var type = context.GetArgument<string>("type");
                    var code = context.GetArgument<string>("code");
                    return accountService.GetAuthResponse(type, code);
                });
            #endregion

            #region Question
            FieldAsync<ListGraphType<QuestionType>>(
                "questions",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CriteriaInputType>> { Name = "criteria" }
                ),
                resolve: context =>
                {
                    var criteria = context.GetArgument<CriteriaModel>("criteria");
                    return questionService.GetQuestionsByCriteria(criteria);
                });

            FieldAsync<IntGraphType>(
                "questionCount",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<CriteriaInputType>> { Name = "criteria" }),
                resolve: context =>
                {
                    var criteria = context.GetArgument<CriteriaModel>("criteria");
                    return questionService.GetQuestionCountByCriteria(criteria);
                });

            FieldAsync<QuestionType>(
                "question",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var questionId = context.GetArgument<int>("id");
                    return questionService.GetQuestion(questionId);
                });

            FieldAsync<ListGraphType<AnswerType>>(
                "questionAnswers",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "questionId" }
                ),
                resolve: context =>
                {
                    var questionId = context.GetArgument<int>("questionId");
                    return questionService.GetAnswersByQuestionId(questionId);
                });

            FieldAsync<ListGraphType<AnswerLikeType>>(
                "answerLikedByQuestion",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "questionId" }
                ),
                resolve: context =>
                {
                    var questionId = context.GetArgument<int>("questionId");
                    return likeRecordService.GetRecordsByQuestion(questionId);
                });

            FieldAsync<AnswerLikeType>(
                "answerLikedByAnswer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "answerId" }
                ),
                resolve: context =>
                {
                    var answerId = context.GetArgument<int>("answerId");
                    return likeRecordService.GetRecordByAnswer(answerId);
                });
            #endregion

            FieldAsync<ListGraphType<CategoryGroupType>>(
                "categoryDefinition",
                resolve: context => CategoryDefinition.CategoryGroups);
        }
    }
}

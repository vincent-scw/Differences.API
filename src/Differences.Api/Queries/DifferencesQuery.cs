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
            ILikeRecordService likeRecordService)
        {
            Name = "DifferencesQuery";

            #region User
            FieldAsync<ListGraphType<UserType>>(
                "topUsers",
                resolve: context => userService.GetTopReputationUsers(1));
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
                "question_count",
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
                "question_answers",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "questionId" }
                ),
                resolve: context =>
                {
                    var questionId = context.GetArgument<int>("questionId");
                    return questionService.GetAnswersByQuestionId(questionId);
                });

            FieldAsync<ListGraphType<AnswerLikeType>>(
                "answer_liked_byquestion",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "questionId" }
                ),
                resolve: context =>
                {
                    var questionId = context.GetArgument<int>("questionId");
                    return likeRecordService.GetRecordsByQuestion(questionId);
                });

            FieldAsync<AnswerLikeType>(
                "answer_liked_byanswer",
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
                "category_definition",
                resolve: context => CategoryDefinition.CategoryGroups);
        }
    }
}

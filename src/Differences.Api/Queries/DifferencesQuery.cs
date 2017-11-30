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
using Differences.Domain.Articles;
using Differences.Interaction.DataTransferModels;
using Differences.Domain;

namespace Differences.Api.Queries
{
    public class DifferencesQuery : ObjectGraphType<object>
    {
        public DifferencesQuery(
            IUserService userService,
            IArticleService articleService,
            IArticleRepository articleRepository,
            IQuestionService questionService,
            IQuestionRepository questionRepository)
        {
            Name = "DifferencesQuery";

            #region User
            FieldAsync<ListGraphType<UserType>>(
                "topUsers",
                resolve: context => userService.GetTopReputationUsers(1));

            FieldAsync<UserType>(
                "checkUserInDb",
                resolve: context =>
                {
                    var user = ((GraphQLUserContext) context.UserContext).UserInfo;
                    return userService.FindOrCreate(user.Id, user.DisplayName, user.Email, user.AvatarUrl);
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
                    return questionRepository.Get(questionId);
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
            #endregion

            #region Article
            FieldAsync<ListGraphType<ArticleType>>(
                "articles",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CriteriaInputType>> { Name = "criteria" }
                ),
                resolve: context =>
                {
                    var criteria = context.GetArgument<CriteriaModel>("criteria");
                    return articleService.GetArticlesByCategory(criteria);
                });

            FieldAsync<IntGraphType>(
                "article_count",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<CriteriaInputType>> { Name = "criteria" }),
                resolve: context =>
                {
                    var criteria = context.GetArgument<CriteriaModel>("criteria");
                    return articleService.GetArticleCountByCategory(criteria);
                });

            FieldAsync<ArticleType>(
                "article",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var articleId = context.GetArgument<int>("id");
                    return articleRepository.Get(articleId);
                });

            FieldAsync<ListGraphType<CommentType>>(
                "article_comments",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "articleId" }
                ),
                resolve: context =>
                {
                    var articleId = context.GetArgument<int>("articleId");
                    return articleService.GetCommentsByArticleId(articleId);
                });
            #endregion

            FieldAsync<ListGraphType<CategoryGroupType>>(
                "category_definition",
                resolve: context =>
                {
                    return CategoryDefinition.CategoryGroups;
                });
        }
    }
}

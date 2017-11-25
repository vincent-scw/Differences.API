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
            Field<ListGraphType<UserType>>(
                "topUsers",
                resolve: context => Task.FromResult(userService.GetTopReputationUsers(1)));

            Field<UserType>(
                "checkUserInDb",
                resolve: context =>
                {
                    var user = ((GraphQLUserContext) context.UserContext).UserInfo;
                    return Task.FromResult(userService.FindOrCreate(user.Id, user.DisplayName, user.Email, user.AvatarUrl));
                });
            #endregion

            #region Question
            Field<ListGraphType<QuestionType>>(
                "questions",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CriteriaInputType>> { Name = "criteria" }
                ),
                resolve: context =>
                {
                    var criteria = context.GetArgument<CriteriaModel>("criteria");
                    return Task.FromResult(questionService.GetQuestionsByCategory(criteria.CategoryId));
                });

            Field<QuestionType>(
                "question",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var questionId = context.GetArgument<int>("id");
                    return Task.FromResult(questionRepository.Get(questionId));
                });

            Field<ListGraphType<AnswerType>>(
                "question_answers",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "questionId" }
                ),
                resolve: context =>
                {
                    var questionId = context.GetArgument<int>("questionId");
                    return Task.FromResult(questionService.GetAnswersByQuestionId(questionId));
                });
            #endregion

            #region Article
            Field<ListGraphType<ArticleType>>(
                "articles",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CriteriaInputType>> { Name = "criteria" }
                ),
                resolve: context =>
                {
                    var criteria = context.GetArgument<CriteriaModel>("criteria");
                    return Task.FromResult(articleService.GetArticlesByCategory(criteria.CategoryId));
                });

            Field<ArticleType>(
                "article",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var articleId = context.GetArgument<int>("id");
                    return Task.FromResult(articleRepository.Get(articleId));
                });

            Field<ListGraphType<CommentType>>(
                "article_comments",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "articleId" }
                ),
                resolve: context =>
                {
                    var articleId = context.GetArgument<int>("articleId");
                    return Task.FromResult(articleService.GetCommentsByArticleId(articleId));
                });
            #endregion
        }
    }
}

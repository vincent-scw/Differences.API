using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Api.Model;
using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using GraphQL.Types;
using Differences.Domain.Questions;
using Differences.Domain.Articles;

namespace Differences.Api.Mutations
{
    public class DifferencesMutation : ObjectGraphType<object>
    {
        public DifferencesMutation(
            IQuestionService questionService,
            IArticleService articleService)
        {
            Name = "DifferencesMutation";

            Field<QuestionType>(
                "submitQuestion",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SubjectInputType>> { Name = "question"}
                ),
                resolve: context =>
                {
                    var user = ((GraphQLUserContext)context.UserContext).UserInfo;
                    var question = context.GetArgument<SubjectModel>("question");
                    return questionService.AskQuestion(question.Title, question.Content, user.Id);
                }
            );

            Field<AnswerType>(
                "submitAnswer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ReplyInputType>> { Name = "answer" }
                ),
                resolve: context =>
                {
                    var user = ((GraphQLUserContext)context.UserContext).UserInfo;
                    var answer = context.GetArgument<ReplyModel>("answer");
                    return questionService.AddAnswer(answer.SubjectId, null, answer.Content, user.Id);
                }
            );

            Field<ArticleType>(
                "submitArticle",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SubjectInputType>> { Name ="article"}
                    ),
                resolve: context =>
                {
                    var user = ((GraphQLUserContext)context.UserContext).UserInfo;
                    var article = context.GetArgument<SubjectModel>("article");
                    return articleService.WriteArticle(article.Title, article.Content, user.Id);
                }
            );

            Field<CommentType>(
                "submitComment",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ReplyInputType>> { Name = "comment" }
                ),
                resolve: context =>
                {
                    var user = ((GraphQLUserContext)context.UserContext).UserInfo;
                    var comment = context.GetArgument<ReplyModel>("comment");
                    return articleService.AddComment(comment.SubjectId, null, comment.Content, user.Id);
                }
            );
        }
    }
}

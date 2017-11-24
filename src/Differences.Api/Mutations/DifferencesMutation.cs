using Differences.Api.Model;
using Differences.Domain.Articles;
using Differences.Domain.Questions;
using Differences.Interaction.DataTransferModels;
using GraphQL.Types;

namespace Differences.Api.Mutations
{
    public class DifferencesMutation : ObjectGraphType<object>
    {
        public DifferencesMutation(
            IQuestionService questionService,
            IArticleService articleService)
        {
            Name = "DifferencesMutation";

            #region Question
            Field<QuestionType>(
                "submitQuestion",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SubjectInputType>> { Name = "question"}
                ),
                resolve: context =>
                {
                    var user = ((GraphQLUserContext)context.UserContext).UserInfo;
                    var question = context.GetArgument<SubjectModel>("question");
                    return question.Id == 0
                        ? questionService.AskQuestion(question.Title, question.Content, user.Id)
                        : questionService.UpdateQuestion(question.Id, question.Title, question.Content, user.Id);
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
                    return answer.Id == 0 
                        ? questionService.AddAnswer(answer.SubjectId, null, answer.Content, user.Id)
                        : questionService.UpdateAnswer(answer.Id, answer.Content, user.Id);
                }
            );
            #endregion

            #region Article
            Field<ArticleType>(
                "submitArticle",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SubjectInputType>> { Name ="article"}
                    ),
                resolve: context =>
                {
                    var user = ((GraphQLUserContext)context.UserContext).UserInfo;
                    var article = context.GetArgument<SubjectModel>("article");
                    return article.Id == 0
                        ? articleService.WriteArticle(article, user.Id)
                        : articleService.UpdateArticle(article, user.Id);
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
                    return comment.Id == 0 
                        ? articleService.AddComment(comment.SubjectId, null, comment.Content, user.Id)
                        : articleService.UpdateComment(comment.Id, comment.Content, user.Id);
                }
            );
            #endregion
        }
    }
}

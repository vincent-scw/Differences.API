using System;
using System.Collections.Generic;
using System.Text;
using Differences.Common;
using Differences.Interaction.DataTransferModels;
using Differences.Interaction.EntityModels;

namespace Differences.Domain.Articles
{
    public interface IArticleService
    {
        IReadOnlyList<Article> GetArticlesByCategory(CriteriaModel criteria);
        int GetArticleCountByCategory(CriteriaModel criteria);
        Article WriteArticle(SubjectModel subject, Guid userGuid);
        Article UpdateArticle(SubjectModel subject, Guid userGuid);

        Comment AddComment(ReplyModel reply, Guid userGuid);
        Comment UpdateComment(ReplyModel reply, Guid userGuid);
        IReadOnlyList<Comment> GetCommentsByArticleId(int articleId);
    }
}

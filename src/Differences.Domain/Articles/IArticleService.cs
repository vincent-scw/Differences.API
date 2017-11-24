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
        IReadOnlyList<Article> GetArticlesByCategory(int categoryId);
        Article WriteArticle(SubjectModel subject, Guid userGuid);
        Article UpdateArticle(SubjectModel subject, Guid userGuid);

        Comment AddComment(int articleId, int? commentId, string content, Guid userGuid);
        Comment UpdateComment(int commentId, string content, Guid userGuid);
    }
}

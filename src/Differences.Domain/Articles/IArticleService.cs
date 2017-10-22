using System;
using System.Collections.Generic;
using System.Text;
using Differences.Common;
using Differences.Interaction.Models;

namespace Differences.Domain.Articles
{
    public interface IArticleService
    {
        IReadOnlyList<Article> GetArticlesByCategory(int categoryId);
        Article WriteArticle(int categoryId, string title, string content, Guid userGuid);
        Article UpdateArticle(int articleId, int categoryId, string title, string content, Guid userGuid);

        Comment AddComment(int articleId, int? commentId, string content, Guid userGuid);
        Comment UpdateComment(int commentId, string content, Guid userGuid);
    }
}

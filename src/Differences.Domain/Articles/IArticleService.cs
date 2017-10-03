using System;
using System.Collections.Generic;
using System.Text;
using Differences.Common;
using Differences.Interaction.Models;

namespace Differences.Domain.Articles
{
    public interface IArticleService
    {
        IReadOnlyList<Article> GetArticlesByCategory(long categoryId);
        Article WriteArticle(string title, string content, Guid userGuid);

        Comment AddComment(long articleId, long? commentId, string content, Guid userGuid);
    }
}

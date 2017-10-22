using Differences.Interaction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
        IReadOnlyList<Comment> GetComments(int articleId);
        Comment GetComment(int commentId);
    }
}

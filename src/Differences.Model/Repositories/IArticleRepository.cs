using Differences.Interaction.EntityModels;
using System.Collections.Generic;

namespace Differences.Interaction.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
        IReadOnlyList<Comment> GetComments(int articleId);
        Comment GetComment(int commentId);
    }
}

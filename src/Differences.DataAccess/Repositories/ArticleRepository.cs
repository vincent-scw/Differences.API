using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Differences.DataAccess.Repositories
{
    public class ArticleRepository : TraceableRepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(DifferencesDbContext dbContext) : base(dbContext)
        {
        }

        protected override Expression<Func<Article, object>>[] DefaultIncludes => new Expression<Func<Article, object>>[]
        {
            (x => x.Author)
        };

        protected override void InsertModifyHistory(Article entity, DataStatus status, Guid? userId)
        {
            DbContext.Set<ArticleUpdateHistory>().Add(new ArticleUpdateHistory(entity.Id, entity.Content, status, userId));
        }

        protected override void InsertRemoveHistory(int id, Guid userId)
        {
            DbContext.Set<ArticleUpdateHistory>().Add(new ArticleUpdateHistory(id, null, DataStatus.Deleted, userId));
        }

        public IReadOnlyList<Comment> GetComments(int articleId)
        {
            return DbContext.Set<Comment>().IncludeEx(x => x.Owner).Where(x => x.ArticleId == articleId).OrderByDescending(x => x.CreateTime).ToList();
        }
    }
}

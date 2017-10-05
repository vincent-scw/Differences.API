using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using System;
using System.Collections.Generic;
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

        protected override void InsertModifyHistory(Article entity, DataStatus status)
        {
            DbContext.Set<ArticleUpdateHistory>().Add(new ArticleUpdateHistory(entity.Id, entity.Content, status));
        }

        protected override void InsertRemoveHistory(int id)
        {
            DbContext.Set<ArticleUpdateHistory>().Add(new ArticleUpdateHistory(id, null, DataStatus.Deleted));
        }
    }
}

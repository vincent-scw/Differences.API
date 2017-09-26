using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using System;
using System.Collections.Generic;
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
        
        protected override void InsertModifyHistory(Article entity, DataStatus status)
        {
            DbContext.Set<ArticleUpdateHistory>().Add(new ArticleUpdateHistory
            {
                ArticleId = entity.Id,
                Content = entity.Content,
                Status = status
            });
        }

        protected override void InsertRemoveHistory(long id)
        {
            DbContext.Set<ArticleUpdateHistory>().Add(new ArticleUpdateHistory
            {
                ArticleId = id,
                Status = DataStatus.Deleted
            });
        }
    }
}

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
        
        protected override void AddModifyHistory(Article entity, DataStatus status)
        {
            DbContext.GetCollection<ArticleUpdateHistory>().InsertOne(new ArticleUpdateHistory
            {
                ArticleId = entity.Id,
                Content = entity.Content,
                Status = status
            });
        }

        protected override Task AddModifyHistoryAsync(Article entity, DataStatus status)
        {
            return DbContext.GetCollection<ArticleUpdateHistory>().InsertOneAsync(new ArticleUpdateHistory
            {
                ArticleId = entity.Id,
                Content = entity.Content,
                Status = status
            });
        }

        protected override void AddRemoveHistory(string id)
        {
            DbContext.GetCollection<ArticleUpdateHistory>().InsertOne(new ArticleUpdateHistory
            {
                ArticleId = id,
                Status = DataStatus.Deleted
            });
        }

        protected override Task AddRemoveHistoryAsync(string id)
        {
            return DbContext.GetCollection<ArticleUpdateHistory>().InsertOneAsync(new ArticleUpdateHistory
            {
                ArticleId = id,
                Status = DataStatus.Deleted
            });
        }
    }
}

using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Differences.DataAccess.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : AggregateRoot
    {
        private readonly DifferencesDbContext _dbContext;

        protected RepositoryBase(IOptions<DbConnectionSetting> settings)
        {
            _dbContext = new DifferencesDbContext(settings);
        }

        public TEntity Get(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", id);
            return _dbContext.GetCollection<TEntity>().Find(filter).FirstOrDefault();
        }

        public IMongoQueryable<TEntity> Find(ISpecification<TEntity> spec)
        {
            return Find(spec.Expression);
        }

        public IMongoQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.GetCollection<TEntity>().AsQueryable().Where(expression);
        }

        public IMongoQueryable<TEntity> GetAll()
        {
            return _dbContext.GetCollection<TEntity>().AsQueryable();
        }

        #region Async
        public Task<TEntity> GetAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", id);
            return _dbContext.GetCollection<TEntity>().Find(filter).FirstOrDefaultAsync();
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.GetCollection<TEntity>().AsQueryable().Where(expression).ToListAsync();
        }

        public Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec)
        {
            return FindAsync(spec.Expression);
        }
        #endregion  

        #region Modify
        public void Add(TEntity entity)
        {
            _dbContext.GetCollection<TEntity>().InsertOne(entity);
        }

        public bool Remove(string id)
        {
            var result = _dbContext.GetCollection<TEntity>().DeleteOne(
                Builders<TEntity>.Filter.Eq("Id", id));
            return result.DeletedCount > 0 && result.IsAcknowledged;
        }

        public bool Update(string id, TEntity entity)
        {
            var result = _dbContext.GetCollection<TEntity>().ReplaceOne(n => n.Id.Equals(id), entity,
                new UpdateOptions {IsUpsert = true});
            return result.ModifiedCount > 0 && result.IsAcknowledged;
        }
        #endregion
    }
}

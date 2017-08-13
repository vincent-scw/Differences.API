using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Differences.DataAccess.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : AggregateRoot
    {
        private readonly DifferencesDbContext _dbContext;
        public RepositoryBase(IOptions<DbConnectionSetting> settings)
        {
            _dbContext = new DifferencesDbContext(settings);
        }

        public Task Add(TEntity entity)
        {
            return _dbContext.GetCollection<TEntity>().InsertOneAsync(entity);
        }

        public Task<List<TEntity>> Find(ISpecification<TEntity> spec)
        {
            return Find(spec.Expression);
        }

        public Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.GetCollection<TEntity>().AsQueryable().Where(expression).ToListAsync();
        }

        public Task<List<TEntity>> GetAll()
        {
            return _dbContext.GetCollection<TEntity>().Find(_ => true).ToListAsync();
        }

        public Task Remove(TEntity entity)
        {
            return Remove(entity.Id);
        }

        public Task Remove(string id)
        {
            return _dbContext.GetCollection<TEntity>().DeleteOneAsync(
              Builders<TEntity>.Filter.Eq("Id", id));
        }

        public Task<TEntity> Single(ISpecification<TEntity> spec)
        {
            return Single(spec.Expression);
        }

        public Task<TEntity> Single(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.GetCollection<TEntity>().AsQueryable().Where(expression).SingleAsync();
        }

        public Task<TEntity> SingleOrDefault(ISpecification<TEntity> spec)
        {
            return SingleOrDefault(spec.Expression);
        }

        public Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.GetCollection<TEntity>().AsQueryable().Where(expression).SingleOrDefaultAsync();
        }

        public Task Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

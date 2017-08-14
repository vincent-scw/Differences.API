using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

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

        public void Add(TEntity entity)
        {
            _dbContext.GetCollection<TEntity>().InsertOne(entity);
        }

        public IQueryable<TEntity> Find(ISpecification<TEntity> spec)
        {
            return Find(spec.Expression);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.GetCollection<TEntity>().AsQueryable().Where(expression);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.GetCollection<TEntity>().AsQueryable();
        }

        public void Remove(TEntity entity)
        {
            Remove(entity.Id);
        }

        public void Remove(string id)
        {
            _dbContext.GetCollection<TEntity>().DeleteOne(
                Builders<TEntity>.Filter.Eq("Id", id));
        }

        public TEntity Single(ISpecification<TEntity> spec)
        {
            return Single(spec.Expression);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.GetCollection<TEntity>().AsQueryable().Where(expression).Single();
        }

        public TEntity SingleOrDefault(ISpecification<TEntity> spec)
        {
            return SingleOrDefault(spec.Expression);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.GetCollection<TEntity>().AsQueryable().Where(expression).SingleOrDefault();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

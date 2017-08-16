﻿using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.IdGenerators;
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
            entity.Id = StringObjectIdGenerator.Instance.GenerateId(_dbContext.GetCollection<TEntity>(), entity) as string;
            _dbContext.GetCollection<TEntity>().InsertOne(entity);
        }

        public void Remove(string id)
        {
            _dbContext.GetCollection<TEntity>().DeleteOne(
                Builders<TEntity>.Filter.Eq("Id", id));
        }

        public void Update(TEntity entity)
        {
            _dbContext.GetCollection<TEntity>().ReplaceOne(n => n.Id.Equals(entity.Id), entity,
                new UpdateOptions {IsUpsert = true});
        }
        #endregion
    }
}
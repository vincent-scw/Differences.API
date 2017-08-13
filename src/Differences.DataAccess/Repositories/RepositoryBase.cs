using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task Add(TEntity entity)
        {
            await _dbContext.GetCollection<TEntity>().InsertOneAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> Find(ISpecification spec)
        {
            //var filter = Builders<TEntity>.Filter.eq
            //return await _dbContext.GetCollection<TEntity>().FindAsync();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.GetCollection<TEntity>().Find(_ => true).ToListAsync();
        }

        public async Task Remove(TEntity entity)
        {
            await this.Remove(entity.Id);
        }

        public async Task Remove(string id)
        {
            await _dbContext.GetCollection<TEntity>().DeleteOneAsync(
              Builders<TEntity>.Filter.Eq("Id", id));
        }

        public Task<TEntity> Single(ISpecification spec)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> SingleOrDefault(ISpecification spec)
        {
            throw new NotImplementedException();
        }

        public Task Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

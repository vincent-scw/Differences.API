using Differences.Interaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace Differences.Interaction.Repositories
{
    public interface IRepository<TEntity> where TEntity : AggregateRoot
    {
        TEntity Get(string id);
        IMongoQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        IMongoQueryable<TEntity> Find(ISpecification<TEntity> spec);
        IMongoQueryable<TEntity> GetAll();

        Task<TEntity> GetAsync(string id);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec);

        void Add(TEntity entity);
        bool Remove(string id);
        bool Update(string id, TEntity entity);
        //void CommitChanges();
    }
}

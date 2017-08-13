using Differences.Interaction.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Differences.Interaction.Repositories
{
    public interface IRepository<TEntity> where TEntity : AggregateRoot
    {
        Task<TEntity> Single(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> Single(ISpecification<TEntity> spec);
        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> SingleOrDefault(ISpecification<TEntity> spec);
        Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> Find(ISpecification<TEntity> spec);
        Task<List<TEntity>> GetAll();

        Task Add(TEntity entity);
        Task Remove(string id);
        Task Remove(TEntity entity);
        Task Update(TEntity entity);
        //void CommitChanges();
    }
}

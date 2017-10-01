using Differences.Interaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Differences.Interaction.Repositories
{
    public interface IRepository<TEntity> where TEntity : AggregateRoot
    {
        TEntity Get(long id);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> Find(ISpecification<TEntity> spec);
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetAsync(long id);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec);

        TEntity Add(TEntity entity);
        long Remove(long id);
        TEntity Update(long id, TEntity entity);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}

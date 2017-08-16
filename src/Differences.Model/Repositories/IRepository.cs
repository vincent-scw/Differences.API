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
        TEntity Get(string id);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> Find(ISpecification<TEntity> spec);
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetAsync(string id);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec);

        void Add(TEntity entity);
        void Remove(string id);
        void Update(TEntity entity);
        //void CommitChanges();
    }
}

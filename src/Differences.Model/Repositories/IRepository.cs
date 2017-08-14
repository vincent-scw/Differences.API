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
        TEntity Single(Expression<Func<TEntity, bool>> expression);
        TEntity Single(ISpecification<TEntity> spec);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression);
        TEntity SingleOrDefault(ISpecification<TEntity> spec);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> Find(ISpecification<TEntity> spec);
        IQueryable<TEntity> GetAll();
            
        void Add(TEntity entity);
        void Remove(string id);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        //void CommitChanges();
    }
}

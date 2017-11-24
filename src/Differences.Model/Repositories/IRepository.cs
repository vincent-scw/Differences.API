using Differences.Interaction.EntityModels;
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
        TEntity Get(int id);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> Find(ISpecification<TEntity> spec);
        IQueryable<TEntity> GetAll();
        bool Exists(int id);

        TEntity Add(TEntity entity);
        int Remove(int id);
        TEntity Update(int id, TEntity entity);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}

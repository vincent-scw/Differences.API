using Differences.Interaction.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Differences.Interaction.Repositories
{
    public interface IRepository
    {
        void UseTransaction(Action action);
        void SaveChanges();
        Task SaveChangesAsync();
    }

    public interface IRepository<TEntity> : IRepository
        where TEntity : AggregateRoot
    {
        TEntity Get(int id);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> Find(ISpecification<TEntity> spec);
        IQueryable<TEntity> GetAll();
        bool Exists(int id);

        TEntity Add(TEntity entity);
        int Remove(int id);
        TEntity Update(int id, TEntity entity);

        void LoadReference<T, TProperty>(T entity, Expression<Func<T, TProperty>> expression)
            where T : class
            where TProperty : class;
    }
}

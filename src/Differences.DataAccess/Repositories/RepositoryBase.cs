using Differences.Interaction.EntityModels;
using Differences.Interaction.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Differences.DataAccess.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : AggregateRoot
    {
        private readonly DifferencesDbContext _dbContext;

        public DbContext DbContext => _dbContext;

        protected RepositoryBase(DifferencesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected virtual Expression<Func<TEntity, object>>[] DefaultIncludes { get; }

        public virtual TEntity Get(int id)
        {
            return _dbContext.Set<TEntity>().IncludeEx(DefaultIncludes).FirstOrDefault(x => x.Id == id);
        }

        public virtual IQueryable<TEntity> Find(ISpecification<TEntity> spec)
        {
            return Find(spec.Expression);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().IncludeEx(DefaultIncludes).Where(expression);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().IncludeEx(DefaultIncludes);
        }

        public bool Exists(int id)
        {
            return _dbContext.Set<TEntity>().Any(x => x.Id == id);
        }

        #region Modify
        public virtual TEntity Add(TEntity entity)
        {
            try
            {
                entity.CreateTime = DateTime.Now;
                
                _dbContext.Set<TEntity>().Add(entity);
                return entity;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public virtual int Remove(int id)
        {
            try
            {
                var set = _dbContext.Set<TEntity>();
                set.Remove(set.Single(x => x.Id == id));
                return id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public virtual TEntity Update(int id, TEntity entity)
        {
            try
            {
                entity.LastUpdateTime = DateTime.Now;

                var e = _dbContext.Set<TEntity>().Update(entity);
                return e.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public virtual void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public virtual Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void LoadReference<T, TProperty>(T entity, Expression<Func<T, TProperty>> expression)
            where T: class
            where TProperty: class 
        => _dbContext.Entry(entity).Reference(expression).Load();
    }
}

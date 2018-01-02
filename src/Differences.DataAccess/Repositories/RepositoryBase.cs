using Differences.Interaction.EntityModels;
using Differences.Interaction.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Differences.DataAccess.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : AggregateRoot
    {
        private readonly DifferencesDbContext _dbContext;

        protected RepositoryBase(DifferencesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected DifferencesDbContext DbContext => _dbContext;

        public virtual TEntity Get(int id)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public virtual IQueryable<TEntity> Find(ISpecification<TEntity> spec)
        {
            return Find(spec.Expression);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return GetAll().Where(expression);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
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

        public void UseTransaction(Action action)
        {
            using (var tran = _dbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    action();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

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

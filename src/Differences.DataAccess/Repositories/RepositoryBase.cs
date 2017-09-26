using Differences.Interaction.Models;
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
    public class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : AggregateRoot
    {
        private readonly DifferencesDbContext _dbContext;

        protected DifferencesDbContext DbContext => _dbContext;

        protected RepositoryBase(DifferencesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual TEntity Get(long id)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public virtual IQueryable<TEntity> Find(ISpecification<TEntity> spec)
        {
            return Find(spec.Expression);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().AsQueryable().Where(expression);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        #region Async retrive
        public virtual Task<TEntity> GetAsync(long id)
        {
            return _dbContext.Set<TEntity>().FirstOrDefaultAsync();
        }

        public virtual Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Where(expression).ToListAsync();
        }

        public virtual Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec)
        {
            return FindAsync(spec.Expression);
        }
        #endregion  

        #region Modify
        public virtual TEntity Add(TEntity entity)
        {
            try
            {
                _dbContext.Set<TEntity>().Add(entity);
                return entity;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public virtual long Remove(long id)
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

        public virtual TEntity Update(long id, TEntity entity)
        {
            try
            {
                var e = _dbContext.Set<TEntity>().Update(entity);
                return e.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

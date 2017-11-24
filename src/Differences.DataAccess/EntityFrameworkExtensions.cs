using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Differences.Interaction.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Differences.DataAccess
{
    public static class EntityFrameworkExtensions
    {
        public static IQueryable<TEntity> IncludeEx<TEntity>(this DbSet<TEntity> dbSet,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : Entity
        {
            IQueryable<TEntity> query = null;
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = dbSet.Include(include);
                }
            }

            return query ?? dbSet;
        }
    }
}

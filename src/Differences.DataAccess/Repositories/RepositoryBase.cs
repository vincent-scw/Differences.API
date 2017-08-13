using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.DataAccess.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : AggregateRoot
    {
        public void CommitChanges()
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> Find(ISpecification spec)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Single(ISpecification spec)
        {
            throw new NotImplementedException();
        }

        public TEntity SingleOrDefault(ISpecification spec)
        {
            throw new NotImplementedException();
        }
    }
}

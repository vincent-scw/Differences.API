using Differences.Interaction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.Repositories
{
    public interface IRepository<TEntity> where TEntity : AggregateRoot
    {
        TEntity Single(ISpecification spec);
        TEntity SingleOrDefault(ISpecification spec);
        IList<TEntity> Find(ISpecification spec);

        void Remove(TEntity entity);

        void CommitChanges();
    }
}

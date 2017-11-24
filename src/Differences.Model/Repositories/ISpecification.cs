using Differences.Interaction.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Differences.Interaction.Repositories
{
    public interface ISpecification
    {
    }

    public interface ISpecification<TEntity> : ISpecification
        where TEntity : AggregateRoot
    {
        Expression<Func<TEntity, bool>> Expression { get; }
    }
}

using Differences.Interaction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Interaction.Repositories
{
    public interface ISpecification
    {
    }

    public interface ISpecification<TEntity> : ISpecification
        where TEntity : AggregateRoot
    {

    }
}

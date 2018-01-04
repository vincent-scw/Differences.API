using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Domain.Policies
{
    public interface IReputationRule
    {
        decimal IncreasingValue { get; }
    }

    public interface IReputationRule<TModel> : IReputationRule
    {
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Domain.Policies
{
    public interface IReputationRule
    {
        double IncreasingValue { get; }
    }

    public interface IReputationRule<TModel> : IReputationRule
    {
        
    }
}

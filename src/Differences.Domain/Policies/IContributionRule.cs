using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Domain.Policies
{
    public interface IContributionRule
    {
        int IncreasingValue { get; }
    }

    public interface IContributionRule<TModel> : IContributionRule
    {
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Domain.Policies
{
    public class LikedReputationRule : IReputationRule
    {
        public decimal IncreasingValue => 0.1M;
    }
}

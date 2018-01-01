using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Domain.Policies
{
    public class NewReplyContributionRule : IContributionRule
    {
        public int IncreasingValue => 1;
    }
}

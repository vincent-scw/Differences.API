using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Domain.Policies
{
    public interface IContributionRule
    {
        int Increase();
        int Decrease();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using GraphQL;

namespace Differences.Common
{
    public class DefinedException : ExecutionError
    {
        public DefinedException(string errorMsg)
            : base(errorMsg)
        {
            
        }
    }
}

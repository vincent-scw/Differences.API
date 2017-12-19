using System;
using System.Collections.Generic;
using System.Text;
using GraphQL;

namespace Differences.Common
{
    public class DefinedException : ExecutionError
    {
        public DefinedException()
            : base(string.Empty)
        {
            
        }
        
        public string ErrorCode { get; set; }
    }
}

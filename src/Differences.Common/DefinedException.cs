using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Common
{
    public class DefinedException : Exception
    {
        public string ErrorCode { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Common.Configuration
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLoader;

namespace Differences.Api
{
    public class GraphQLUserContext
    {
        public DataLoaderContext LoadContext { get; }

        public ClaimsPrincipal User { get; }

        public GraphQLUserContext(DataLoaderContext loadContext,
            ClaimsPrincipal user)
        {
            LoadContext = loadContext;
            User = user;
        }
    }
}

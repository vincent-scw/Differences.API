using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLoader;
using Differences.Api.Model;
using Differences.Common;

namespace Differences.Api
{
    public class GraphQLUserContext
    {
        public DataLoaderContext LoadContext { get; }

        public bool IsAuthenticated { get; }

        public GraphQLUserContext(DataLoaderContext loadContext,
            ClaimsPrincipal user)
        {
            LoadContext = loadContext;
            IsAuthenticated = user.Identity.IsAuthenticated;
        }
    }
}

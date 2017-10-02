using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLoader;
using Differences.Api.Model;

namespace Differences.Api
{
    public class GraphQLUserContext
    {
        public DataLoaderContext LoadContext { get; }

        public UserInfo UserInfo { get; }

        public bool IsAuthenticated { get; }

        public GraphQLUserContext(DataLoaderContext loadContext,
            ClaimsPrincipal user)
        {
            LoadContext = loadContext;
            IsAuthenticated = user.Identity.IsAuthenticated;

            if (IsAuthenticated)
                UserInfo = new UserInfo(user);
        }
    }
}

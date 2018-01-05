using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Domain.Models;
using GraphQL.Types;

namespace Differences.Api.Model
{
    public class AuthResponseType : ObjectGraphType<UserWithTokenModel>
    {
        public AuthResponseType()
        {
            Field(x => x.AccessToken);
            Field< UserType>("user", resolve: context => context.Source.User);
        }
    }
}

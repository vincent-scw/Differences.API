using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Api.Model;
using Differences.Interaction.Models;
using GraphQL.Types;

namespace Differences.Api.Queries
{
    public class DifferencesQuery : ObjectGraphType<object>
    {
        public DifferencesQuery()
        {
            Field<UserType>(
                "user",
                resolve: context => Task.FromResult(
                    new User {Id = "12345", Name = "test", DisplayName = "this is test"}));
        }
    }
}

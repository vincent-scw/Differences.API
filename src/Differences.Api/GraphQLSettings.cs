using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace Differences.Api
{
    public class GraphQLSettings
    {
        public PathString Path { get; set; } = "/api/graphql";
        public ISchema Schema { get; set; }
    }
}

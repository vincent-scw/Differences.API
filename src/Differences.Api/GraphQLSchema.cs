using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Api.Queries;
using GraphQL.Types;

namespace Differences.Api
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(Func<Type, GraphType> resolveType)
            : base(resolveType)
        {
            Query = (DifferencesQuery) resolveType(typeof(DifferencesQuery));
        }
    }
}

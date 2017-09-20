using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Api.Mutations;
using Differences.Api.Queries;
using GraphQL;
using GraphQL.Types;

namespace Differences.Api
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<DifferencesQuery>();
            Mutation = resolver.Resolve<DifferencesMutation>();
        }
    }
}

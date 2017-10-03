using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Differences.Api.Model
{
    public class CriteriaInputType : InputObjectGraphType
    {
        public CriteriaInputType()
        {
            Name = "FetchInput";
            Field<NonNullGraphType<IntGraphType>>("categoryId");
            Field<IntGraphType>("offset");
            Field<IntGraphType>("count");
        }
    }
}

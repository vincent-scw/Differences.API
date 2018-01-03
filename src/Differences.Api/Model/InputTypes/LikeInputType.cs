using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace Differences.Api.Model
{
    public class LikeInputType : InputObjectGraphType
    {
        public LikeInputType()
        {
            Name = "LikeInput";

            Field<NonNullGraphType<IntGraphType>>("questionId");
            Field<NonNullGraphType<IntGraphType>>("answerId");
        }
    }
}

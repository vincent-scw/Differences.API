using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Differences.Api.Model
{
    public class ReplyInputType : InputObjectGraphType
    {
        public ReplyInputType()
        {
            Name = "ReplyInput";

            Field<NonNullGraphType<IntGraphType>>("subjectId");
            Field<NonNullGraphType<StringGraphType>>("content");
            Field<IntGraphType>("parentId");
        }
    }
}

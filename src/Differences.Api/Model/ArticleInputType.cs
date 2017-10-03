using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Differences.Api.Model
{
    public class ArticleInputType : InputObjectGraphType
    {
        public ArticleInputType()
        {
            Name = "ArticleInput";

            Field<NonNullGraphType<StringGraphType>>("title");
            Field<NonNullGraphType<StringGraphType>>("content");
            Field<NonNullGraphType<IntGraphType>>("categoryId");
        }
    }
}

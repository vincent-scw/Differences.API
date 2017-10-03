﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace Differences.Api.Model
{
    public class QuestionInputType : InputObjectGraphType
    {
        public QuestionInputType()
        {
            Name = "QuestionInput";
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<NonNullGraphType<StringGraphType>>("content");
            Field<NonNullGraphType<IntGraphType>>("categoryId");
        }
    }
}

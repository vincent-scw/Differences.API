using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace Differences.Api.Model
{
    public class UserInputType : InputObjectGraphType
    {
        public UserInputType()
        {
            Name = "UserInput";
            Field<NonNullGraphType<StringGraphType>>("displayName");
            Field<StringGraphType>("email");
            Field<NonNullGraphType<BooleanGraphType>>("hideAvatar");
        }
    }
}

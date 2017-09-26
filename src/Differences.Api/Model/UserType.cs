using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Interaction.Models;
using GraphQL.Types;

namespace Differences.Api.Model
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.DisplaykName);
            Field(x => x.AvatarUrl);
        }
    }
}

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
            Field("id", x => x.Id.ToString());
            Field(x => x.Email);
            Field(x => x.DisplayName);
            Field(x => x.AvatarUrl, nullable: true);
        }
    }
}

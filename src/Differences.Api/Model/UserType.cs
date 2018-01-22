using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Interaction.EntityModels;
using GraphQL.Types;

namespace Differences.Api.Model
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field("id", x => x.Id.ToString());
            Field(x => x.Email, nullable: true);
            Field(x => x.DisplayName);
            Field(x => x.AvatarUrl, nullable: true);
            Field(x => x.HideAvatar);
            Field(x => x.UserScores.ContributeValue);
            Field(x => x.UserScores.ReputationValue);
        }
    }
}

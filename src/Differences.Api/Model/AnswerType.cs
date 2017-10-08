using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Interaction.Models;
using GraphQL.Types;

namespace Differences.Api.Model
{
    public class AnswerType : ObjectGraphType<Answer>
    {
        public AnswerType()
        {
            Field(x => x.Id);
            Field(x => x.Content);
            Field(x => x.CreateTime);
            Field(x => x.LastUpdateTime, nullable: true);

            Field<UserType>("user", resolve: context => context.Source.Owner);
        }
    }
}

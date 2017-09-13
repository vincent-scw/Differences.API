using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Interaction.Models;
using GraphQL.Types;

namespace Differences.Api.Model
{
    public class ReplyType : ObjectGraphType<Reply>
    {
        public ReplyType()
        {
            Field(x => x.Id);
        }
    }
}

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
        }
    }
}

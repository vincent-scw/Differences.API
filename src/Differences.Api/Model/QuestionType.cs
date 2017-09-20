using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Interaction.Models;
using GraphQL.Types;

namespace Differences.Api.Model
{
    public class QuestionType : ObjectGraphType<Question>
    {
        public QuestionType()
        {
            Field(x => x.Id).Description("The id of the question");
            Field(x => x.Title).Description("The title of the question");
            Field(x => x.Content).Description("The content of the question");
        }
    }
}

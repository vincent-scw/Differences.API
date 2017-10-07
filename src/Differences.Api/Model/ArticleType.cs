using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Interaction.Models;
using GraphQL.Types;

namespace Differences.Api.Model
{
    public class ArticleType : ObjectGraphType<Article>
    {
        public ArticleType()
        {
            Field(x => x.Id).Description("The id of the Article");
            Field(x => x.Title).Description("The title of the Article");
            Field(x => x.Content).Description("The content of the Article");
            Field<UserType>("user", resolve: context => context.Source.Author);
        }
    }
}

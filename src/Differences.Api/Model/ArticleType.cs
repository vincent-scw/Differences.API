﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Domain;
using Differences.Interaction.EntityModels;
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
            Field(x => x.CreateTime);
            Field(x => x.LastUpdateTime, nullable: true);

            Field<StringGraphType>("category",
                resolve: context => CategoryDefinition.GetCategoryString(context.Source.CategoryId));
            Field<UserType>("user", resolve: context => context.Source.Author);
        }
    }
}

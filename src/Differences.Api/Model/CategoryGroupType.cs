using System;
using Differences.Interaction.EntityModels;
using GraphQL.Types;

namespace Differences.Api.Model
{
    public class CategoryGroupType : ObjectGraphType<CategoryGroup>
    {
        public CategoryGroupType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Description);

            Field<ListGraphType<CategoryType>>(
                "categories",
                resolve: context => context.Source.Categories
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Domain;
using Differences.Domain.Models;
using Differences.Interaction.EntityModels;
using GraphQL.Types;

namespace Differences.Api.Model
{
    public class QuestionType : ObjectGraphType<QuestionModel>
    {
        public QuestionType()
        {
            Field(x => x.Id).Description("The id of the question");
            Field(x => x.Title).Description("The title of the question");
            Field(x => x.Content).Description("The content of the question");
            Field(x => x.CreateTime);
            Field(x => x.LastUpdateTime, nullable: true);
            Field(x => x.CategoryId);
            Field(x => x.CategoryName);
            Field(x => x.AnswerCount);
            Field<UserType>("user", resolve: context => context.Source.User);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Domain.Models;
using GraphQL.Types;

namespace Differences.Api.Model
{
    public class AnswerLikeType : ObjectGraphType<AnswerLikeModel>
    {
        public AnswerLikeType()
        {
            Field(x => x.AnswerId);
            Field(x => x.LikeCount);
            Field(x => x.Liked);
        }
    }
}

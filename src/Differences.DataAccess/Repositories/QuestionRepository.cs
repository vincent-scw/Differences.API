using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using Microsoft.Extensions.Options;

namespace Differences.DataAccess.Repositories
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        public QuestionRepository(DifferencesDbContext dbContext) : base(dbContext)
        {
        }

        protected override Expression<Func<Question, object>>[] DefaultIncludes => new Expression<Func<Question, object>>[]
        {
            (x => x.Owner)
        };
    }
}

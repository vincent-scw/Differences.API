using System;
using System.Collections.Generic;
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
    }
}

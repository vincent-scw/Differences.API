﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public IReadOnlyList<Answer> GetAnswers(int questionId)
        {
            return DbContext.Set<Answer>().IncludeEx(x => x.Owner).Where(x => x.QuestionId == questionId).OrderByDescending(x => x.CreateTime).ToList();
        }

        public Answer GetAnswer(int answerId)
        {
            var retval = DbContext.Set<Answer>().IncludeEx(x => x.Owner).FirstOrDefault(x => x.Id == answerId);
            return retval;
        }
    }
}

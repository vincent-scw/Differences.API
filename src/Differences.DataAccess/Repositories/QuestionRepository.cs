using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Differences.Interaction.EntityModels;
using Differences.Interaction.Repositories;
using Microsoft.EntityFrameworkCore;
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
            return DbContext.Set<Answer>()
                            .Include(x => x.Owner)
                            .Include(x => x.SubAnswers)
                                .ThenInclude(answer => answer.Owner)
                            .Where(x => x.QuestionId == questionId && x.ParentReplyId == null).ToList();
        }

        public Answer GetAnswer(int answerId)
        {
            var retval = DbContext.Set<Answer>()
                            .Include(x => x.Owner)
                            .Include(x => x.SubAnswers)
                                .ThenInclude(answer => answer.Owner)
                            .FirstOrDefault(x => x.Id == answerId);
            return retval;
        }
    }
}

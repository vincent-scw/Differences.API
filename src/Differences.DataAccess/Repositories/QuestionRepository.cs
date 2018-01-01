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

        public override Question Get(int id)
        {
            return DbContext
                .Set<Question>()
                    .Include(x => x.Owner)
                        .ThenInclude(x => x.UserScores).FirstOrDefault(x => x.Id == id);
        }

        public override IQueryable<Question> GetAll()
        {
            return DbContext.Set<Question>()
                    .Include(x => x.Owner)
                        .ThenInclude(x => x.UserScores);
        }

        public IReadOnlyList<Answer> GetAnswers(int questionId)
        {
            return DbContext.Set<Answer>()
                            .Include(x => x.Owner)
                                .ThenInclude(o => o.UserScores)
                            .Include(x => x.SubAnswers)
                                .ThenInclude(answer => answer.Owner)
                                    .ThenInclude(owner => owner.UserScores)
                            .Where(x => x.QuestionId == questionId && x.ParentReplyId == null).ToList();
        }

        public Answer GetAnswer(int answerId)
        {
            var retval = DbContext.Set<Answer>()
                            .Include(x => x.Owner)
                                .ThenInclude(o => o.UserScores)
                            .Include(x => x.SubAnswers)
                                .ThenInclude(answer => answer.Owner)
                                    .ThenInclude(owner => owner.UserScores)
                            .FirstOrDefault(x => x.Id == answerId);
            return retval;
        }
    }
}

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
                .Questions
                    .Include(x => x.Owner)
                        .ThenInclude(x => x.UserScores).SingleOrDefault(x => x.Id == id);
        }

        public override IQueryable<Question> GetAll()
        {
            return DbContext.Questions
                    .Include(x => x.Owner)
                        .ThenInclude(x => x.UserScores);
        }

        public IReadOnlyList<Answer> GetAnswers(int questionId)
        {
            return DbContext.Answers
                            .Include(x => x.Owner)
                                .ThenInclude(o => o.UserScores)
                            .Include(x => x.SubAnswers)
                                .ThenInclude(answer => answer.Owner)
                                    .ThenInclude(owner => owner.UserScores)
                            .Where(x => x.QuestionId == questionId && x.ParentReplyId == null).ToList();
        }

        public Answer GetAnswer(int answerId)
        {
            var retval = DbContext.Answers
                            .Include(x => x.Owner)
                                .ThenInclude(o => o.UserScores)
                            .Include(x => x.SubAnswers)
                                .ThenInclude(answer => answer.Owner)
                                    .ThenInclude(owner => owner.UserScores)
                            .SingleOrDefault(x => x.Id == answerId);
            return retval;
        }

        public User GetAnswerOwner(int answerId)
        {
            var user = DbContext.Answers
                .Include(x => x.Owner)
                .Where(x => x.Id == answerId)
                .Select(x => x.Owner)
                .SingleOrDefault();
            LoadReference(user, x => x.UserScores);
            return user;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Differences.Common;
using Differences.Common.Resources;
using Differences.Domain.Models;
using Differences.Domain.Policies;
using Differences.Interaction.DataTransferModels;
using Differences.Interaction.EntityModels;
using Differences.Interaction.Repositories;
using Microsoft.Extensions.Localization;

namespace Differences.Domain.LikeRecords
{
    public class LikeRecordService : ServiceBase, ILikeRecordService
    {
        private readonly ILikeRecordRepository _likeRecordRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;

        public LikeRecordService(
            ILikeRecordRepository likeRecordRepository,
            IQuestionRepository questionRepository,
            IUserRepository userRepository,
            IUserContextService userContextService, 
            IStringLocalizer<Errors> localizer)
            : base(userContextService, localizer)
        {
            _likeRecordRepository = likeRecordRepository;
            _questionRepository = questionRepository;
            _userRepository = userRepository;
        }

        public IReadOnlyList<AnswerLikeModel> GetRecordsByQuestion(int questionId)
        {
            var query = from answer in _questionRepository.GetAnswersQuery(questionId)
                join record in _likeRecordRepository.GetAll()
                on answer.Id equals record.AnswerId into temp
                from tt in temp.DefaultIfEmpty()
                group new {answer, tt} by answer.Id
                into g
                select new AnswerLikeModel
                {
                    AnswerId = g.Key,
                    LikeCount = g.Count(x => x.tt != null),
                    Liked = g.Any(x => x.tt != null && x.tt.UserId == UserId)
                };

            return query.ToList();
        }

        public AnswerLikeModel GetRecordByAnswer(int answerId)
        {
            var query = from record in _likeRecordRepository.GetAll()
                group record by record.AnswerId
                into g
                where g.Key == answerId
                select new AnswerLikeModel
                {
                    AnswerId = g.Key,
                    LikeCount = g.Count(),
                    Liked = g.Any(x => x.UserId == UserId)
                };

            return query.SingleOrDefault();
        }

        public User AddRecord(LikeRecordModel model)
        {
            User owner = null;
            _likeRecordRepository.UseTransaction(() =>
            {
                var record = new LikeRecord(UserId, model.QuestionId, model.AnswerId);
                _likeRecordRepository.Add(record);
                _likeRecordRepository.SaveChanges();

                // Add reputation value to answer owner
                owner = _questionRepository.GetAnswerOwner(model.AnswerId);
                if (owner == null)
                    throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.UserNotFound));

                owner.UserScores.IncreaseReputation((int) ReputationTypeDefinition.Liked,
                    new LikedReputationRule().IncreasingValue, model.QuestionId);  // use questionId here to add log, then the log will be '您关于XX问题的回答获得了好评'
                _userRepository.SaveChanges();
            });

            return owner;
        }

        public bool LikedBy(Guid userId, int answerId)
        {
            var query = from record in _likeRecordRepository.GetAll()
                where record.AnswerId == answerId
                where record.UserId == UserId
                select record.Id;

            return query.Any();
        }
    }
}

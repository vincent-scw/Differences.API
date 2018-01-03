using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Differences.Common;
using Differences.Common.Resources;
using Differences.Domain.Policies;
using Differences.Interaction.EntityModels;
using Differences.Interaction.Repositories;
using Microsoft.Extensions.Localization;

namespace Differences.Domain.LikeRecords
{
    public class LikeRecordService : ServiceBase, ILikeRecordService
    {
        private readonly ILikeRecordRepository _likeRecordRepository;
        private readonly IUserRepository _userRepository;

        public LikeRecordService(
            ILikeRecordRepository likeRecordRepository,
            IUserRepository userRepository,
            IUserContextService userContextService, 
            IStringLocalizer<Errors> localizer)
            : base(userContextService, localizer)
        {
            _likeRecordRepository = likeRecordRepository;
            _userRepository = userRepository;
        }

        public IReadOnlyList<int> GetRecordsByQuestion(int questionId)
        {
            if (UserId == Guid.Empty)
                return new List<int>();

            var query = from record in _likeRecordRepository.GetAll()
                where record.QuestionId == questionId
                where record.UserId == UserId
                select record.AnswerId;

            return query.ToList();
        }

        public void AddRecord(int questionId, int answerId)
        {
            _likeRecordRepository.UseTransaction(() =>
            {
                var user = _userRepository.Get(UserId);
                if (user == null)
                    throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.UserNotFound));

                var record = new LikeRecord(UserId, questionId, answerId);
                _likeRecordRepository.Add(record);
                _likeRecordRepository.SaveChanges();

                user.UserScores.IncreaseReputation((int) ReputationTypeDefinition.Liked,
                    new LikedReputationRule().IncreasingValue, questionId);  // use questionId here to add log, then the log will be '您关于XX问题的回答获得了好评'
                _userRepository.SaveChanges();
            });
        }
    }
}

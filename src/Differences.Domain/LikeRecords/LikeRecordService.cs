using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Domain.LikeRecords
{
    public class LikeRecordService : ILikeRecordService
    {
        public List<int> GetRecordsByQuestion(Guid userId, int questionId)
        {
            throw new NotImplementedException();
        }

        public bool AddRecord(Guid userId, int questionId, int answerId)
        {
            throw new NotImplementedException();
        }
    }
}

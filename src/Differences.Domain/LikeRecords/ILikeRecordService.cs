using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Domain.LikeRecords
{
    public interface ILikeRecordService
    {
        List<int> GetRecordsByQuestion(Guid userId, int questionId);
        bool AddRecord(Guid userId, int questionId, int answerId);
    }
}

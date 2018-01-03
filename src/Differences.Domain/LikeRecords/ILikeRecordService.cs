using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.Domain.LikeRecords
{
    public interface ILikeRecordService
    {
        IReadOnlyList<int> GetRecordsByQuestion(int questionId);
        void AddRecord(int questionId, int answerId);
    }
}

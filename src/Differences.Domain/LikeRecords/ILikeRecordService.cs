using System;
using System.Collections.Generic;
using System.Text;
using Differences.Domain.Models;
using Differences.Interaction.DataTransferModels;
using Differences.Interaction.EntityModels;

namespace Differences.Domain.LikeRecords
{
    public interface ILikeRecordService
    {
        IReadOnlyList<AnswerLikeModel> GetRecordsByQuestion(int questionId);
        AnswerLikeModel GetRecordByAnswer(int answerId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Answer owner</returns>
        User AddRecord(LikeRecordModel model);

        bool LikedBy(Guid userId, int answerId);
    }
}

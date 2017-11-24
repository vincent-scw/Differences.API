﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Differences.Interaction.EntityModels;

namespace Differences.Domain.Questions
{
    public interface IQuestionService
    {
        Question AskQuestion(string title, string content, Guid userGuid);
        Question UpdateQuestion(int questionId, string title, string content, Guid userGuid);
        IReadOnlyList<Question> GetQuestionsByCategory(int categoryId);
        IReadOnlyList<Answer> GetAnswersByQuestionId(int questionId);

        Answer AddAnswer(int questionId, int? parentReplyId, string content, Guid userGuid);
        Answer UpdateAnswer(int answerId, string content, Guid userGuid);
    }
}

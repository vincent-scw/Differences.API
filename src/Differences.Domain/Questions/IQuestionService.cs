using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Differences.Interaction.Models;

namespace Differences.Domain.Questions
{
    public interface IQuestionService
    {
        Question AskQuestion(string title, string content, Guid userGuid);
        IReadOnlyList<Question> GetQuestionsByCategory(int categoryId);

        Answer AddAnswer(int questionId, int? parentReplyId, string content, Guid userGuid);
    }
}

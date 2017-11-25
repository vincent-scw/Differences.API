using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Differences.Interaction.DataTransferModels;
using Differences.Interaction.EntityModels;

namespace Differences.Domain.Questions
{
    public interface IQuestionService
    {
        Question AskQuestion(SubjectModel subject, Guid userGuid);
        Question UpdateQuestion(SubjectModel subject, Guid userGuid);
        IReadOnlyList<Question> GetQuestionsByCategory(int categoryId);
        IReadOnlyList<Answer> GetAnswersByQuestionId(int questionId);

        Answer AddAnswer(ReplyModel reply, Guid userGuid);
        Answer UpdateAnswer(ReplyModel reply, Guid userGuid);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Differences.Domain.Models;
using Differences.Interaction.DataTransferModels;
using Differences.Interaction.EntityModels;

namespace Differences.Domain.Questions
{
    public interface IQuestionService
    {
        QuestionModel GetQuestion(int questionId);
        QuestionModel AskQuestion(SubjectModel subject);
        QuestionModel UpdateQuestion(SubjectModel subject);
        IReadOnlyList<QuestionModel> GetQuestionsByCriteria(CriteriaModel criteria);
        int GetQuestionCountByCriteria(CriteriaModel criteria);
        IReadOnlyList<Answer> GetAnswersByQuestionId(int questionId);

        Answer AddAnswer(ReplyModel reply);
        Answer UpdateAnswer(ReplyModel reply);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Differences.Interaction.Models;

namespace Differences.Interaction.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        IReadOnlyList<Answer> GetAnswers(int questionId);
    }
}

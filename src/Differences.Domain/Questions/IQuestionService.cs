using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Differences.Interaction.Models;

namespace Differences.Domain.Questions
{
    public interface IQuestionService
    {
        Question AskQuestion(string title, IEnumerable<string> tags, string content);
    }
}

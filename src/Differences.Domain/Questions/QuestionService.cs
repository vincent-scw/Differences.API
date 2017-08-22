using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Differences.Interaction.Models;
using Differences.Interaction.Repositories;

namespace Differences.Domain.Questions
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async void AskQuestionAsync(string title, IEnumerable<string> tags, string content)
        {
            await _questionRepository.AddAsync(new Question
            {
                Title = title,
                Content = content
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Differences.Common;
using Differences.Domain.Users;
using Differences.Interaction.Models;
using Differences.Interaction.Repositories;

namespace Differences.Domain.Questions
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;

        public QuestionService(
            IQuestionRepository questionRepository,
            IUserRepository userRepository)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
        }

        public Question AskQuestion(string title, string content, Guid userGuid)
        {
            if (!_userRepository.Exists(userGuid))
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var result = _questionRepository.Add(new Question(title, content, userGuid));
            _questionRepository.SaveChanges();
            return result;
        }

        public IReadOnlyList<Question> GetQuestionsByCategory(int categoryId)
        {
            return _questionRepository.GetAll().ToList();
        }

        public Answer AddAnswer(int questionId, int? parentReplyId, string content, Guid userGuid)
        {
            var question = _questionRepository.Get(questionId);
            if (question == null)
                throw new DefinedException {ErrorCode = ErrorDefinitions.Question.QuestionNotExists};

            if (!_userRepository.Exists(userGuid))
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var reply = new Answer(questionId, parentReplyId, content, userGuid);
            question.AddAnswer(reply);

            _questionRepository.SaveChanges();
            return reply;
        }
    }
}

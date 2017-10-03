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
        private readonly IUserService _userService;

        public QuestionService(
            IQuestionRepository questionRepository,
            IUserService userService)
        {
            _questionRepository = questionRepository;
            _userService = userService;
        }

        public Question AskQuestion(string title, string content, Guid userGuid)
        {
            var user = _userService.GetUserInfo(userGuid);
            if (user == null)
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            return _questionRepository.Add(new Question(title, content, user.Id));
        }

        public IReadOnlyList<Question> GetQuestionsByCategory(int categoryId)
        {
            return _questionRepository.GetAll().ToList();
        }

        public Reply AddReply(int questionId, int? parentReplyId, string content, Guid userGuid)
        {
            var question = _questionRepository.Get(questionId);
            if (question == null)
                throw new DefinedException {ErrorCode = ErrorDefinitions.Question.QuestionNotExists};

            var user = _userService.GetUserInfo(userGuid);
            if (user == null)
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var reply = new Reply(questionId, parentReplyId, content, user.Id);
            question.AddReply(reply);

            return reply;
        }
    }
}

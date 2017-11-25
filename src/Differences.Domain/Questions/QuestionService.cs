using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Differences.Common;
using Differences.Domain.Users;
using Differences.Interaction.EntityModels;
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

        public Question UpdateQuestion(int questionId, string title, string content, Guid userGuid)
        {
            if (!_userRepository.Exists(userGuid))
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var question = _questionRepository.Get(questionId);
            if (question == null)
                throw new DefinedException {ErrorCode = ErrorDefinitions.Question.QuestionNotExists};

            if (question.OwnerId != userGuid)
                throw new DefinedException {ErrorCode = ErrorDefinitions.User.AccessDenied};

            question.Update(title, content);
            _questionRepository.SaveChanges();
            return question;
        }

        public IReadOnlyList<Question> GetQuestionsByCategory(int categoryId)
        {
            var categoryGroup = CategoryDefinition.GetCategoryGroup(categoryId);
            if (categoryGroup == null)
                return _questionRepository.GetAll().Where(x => x.CategoryId == categoryId)
                    .OrderByDescending(x => x.CreateTime).ToList();
            else
            {
                var ids = categoryGroup.Categories.Select(x => x.Id);
                return _questionRepository.GetAll().Where(x => ids.Contains(x.CategoryId))
                    .OrderByDescending(x => x.CreateTime).ToList();
            }
        }

        public IReadOnlyList<Answer> GetAnswersByQuestionId(int questionId)
        {
            var answers = _questionRepository.GetAnswers(questionId);
            var firstLevel = answers.Where(x => x.ParentReplyId == null).ToList();
            var secondLevel = answers.Where(x => x.ParentReplyId != null).ToList();

            firstLevel.ForEach(f =>
            {
                f.SubAnswers.AddRange(secondLevel.Where(s => s.ParentReplyId == f.Id).OrderBy(x => x.CreateTime));
            });

            return firstLevel.OrderByDescending(x => x.CreateTime).ToList();
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

        public Answer UpdateAnswer(int answerId, string content, Guid userGuid)
        {
            var answer = _questionRepository.GetAnswer(answerId);
            if (answer == null)
                throw new DefinedException {ErrorCode = ErrorDefinitions.Question.AnswerNotExists};

            if (answer.OwnerId != userGuid)
                throw new DefinedException {ErrorCode = ErrorDefinitions.User.AccessDenied};

            answer.Update(content);
            _questionRepository.SaveChanges();
            return answer;
        }
    }
}

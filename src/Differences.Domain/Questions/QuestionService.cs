using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Differences.Common;
using Differences.Domain.Users;
using Differences.Interaction.DataTransferModels;
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

        public Question AskQuestion(SubjectModel subject, Guid userGuid)
        {
            if (!_userRepository.Exists(userGuid))
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var result = _questionRepository.Add(new Question(subject.Title, subject.Content, subject.CategoryId, userGuid));
            _questionRepository.SaveChanges();

            _questionRepository.LoadReference(result, x => x.Owner);
            return result;
        }

        public Question UpdateQuestion(SubjectModel subject, Guid userGuid)
        {
            if (!_userRepository.Exists(userGuid))
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var question = _questionRepository.Get(subject.Id);
            if (question == null)
                throw new DefinedException {ErrorCode = ErrorDefinitions.Question.QuestionNotExists};

            if (question.OwnerId != userGuid)
                throw new DefinedException {ErrorCode = ErrorDefinitions.User.AccessDenied};

            question.Update(subject.Title, subject.Content, subject.CategoryId);
            _questionRepository.SaveChanges();

            _questionRepository.LoadReference(question, x => x.Owner);
            return question;
        }

        public IReadOnlyList<Question> GetQuestionsByCriteria(CriteriaModel criteria)
        {
            return GetSearchQuery(criteria.CategoryId).Skip(criteria.Offset ?? 0).Take(criteria.Limit ?? 0).ToList();
        }

        public int GetQuestionCountByCriteria(CriteriaModel criteria)
        {
            return GetSearchQuery(criteria.CategoryId).Count();
        }

        private IQueryable<Question> GetSearchQuery(int categoryId)
        {
            var categoryGroup = CategoryDefinition.GetCategoryGroup(categoryId);
            if (categoryGroup == null)
                return _questionRepository.GetAll().Where(x => x.CategoryId == categoryId)
                    .OrderByDescending(x => x.CreateTime);
            else
            {
                var ids = categoryGroup.Categories.Select(x => x.Id);
                return _questionRepository.GetAll().Where(x => ids.Contains(x.CategoryId))
                                          .OrderByDescending(x => x.CreateTime);
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

        public Answer AddAnswer(ReplyModel reply, Guid userGuid)
        {
            var question = _questionRepository.Get(reply.SubjectId);
            if (question == null)
                throw new DefinedException {ErrorCode = ErrorDefinitions.Question.QuestionNotExists};

            if (!_userRepository.Exists(userGuid))
                throw new DefinedException { ErrorCode = ErrorDefinitions.User.UserNotFound };

            var answer = new Answer(reply.SubjectId, reply.ParentId, reply.Content, userGuid);
            question.AddAnswer(answer);

            _questionRepository.SaveChanges();

            _questionRepository.LoadReference(answer, x => x.Owner);
            return answer;
        }

        public Answer UpdateAnswer(ReplyModel reply, Guid userGuid)
        {
            var answer = _questionRepository.GetAnswer(reply.Id);
            if (answer == null)
                throw new DefinedException {ErrorCode = ErrorDefinitions.Question.AnswerNotExists};

            if (answer.OwnerId != userGuid)
                throw new DefinedException {ErrorCode = ErrorDefinitions.User.AccessDenied};

            answer.Update(reply.Content);
            _questionRepository.SaveChanges();

            _questionRepository.LoadReference(answer, x => x.Owner);
            _questionRepository.LoadReference(answer, x => x.SubAnswers);
            return answer;
        }
    }
}

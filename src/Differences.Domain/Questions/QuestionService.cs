using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Differences.Common;
using Differences.Common.Resources;
using Differences.Domain.Users;
using Differences.Domain.Validators;
using Differences.Interaction.DataTransferModels;
using Differences.Interaction.EntityModels;
using Differences.Interaction.Repositories;
using Microsoft.Extensions.Localization;

namespace Differences.Domain.Questions
{
    public class QuestionService : ServiceBase, IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;

        public QuestionService(
            IQuestionRepository questionRepository,
            IUserRepository userRepository,
            IStringLocalizer<Errors> localizer)
            : base(localizer)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
        }

        public Question AskQuestion(SubjectModel subject, Guid userGuid)
        {
            if (!_userRepository.Exists(userGuid))
                throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.UserNotFound));

            if (!new SubjectValidator(subject).Validate(out string errorCode))
                throw new DefinedException(GetLocalizedResource(errorCode));

            var result = _questionRepository.Add(new Question(subject.Title, subject.Content, subject.CategoryId, userGuid));
            _questionRepository.SaveChanges();

            _questionRepository.LoadReference(result, x => x.Owner);
            return result;
        }

        public Question UpdateQuestion(SubjectModel subject, Guid userGuid)
        {
            if (!_userRepository.Exists(userGuid))
                throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.UserNotFound));

            var question = _questionRepository.Get(subject.Id);
            if (question == null)
                throw new DefinedException(GetLocalizedResource(ErrorDefinitions.Question.QuestionNotExists));

            if (question.OwnerId != userGuid)
                throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.AccessDenied));

            if (!new SubjectValidator(subject).Validate(out string errorCode))
                throw new DefinedException(GetLocalizedResource(errorCode));

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

            return answers.OrderByDescending(x => x.CreateTime).ToList();
        }

        public Answer AddAnswer(ReplyModel reply, Guid userGuid)
        {
            var question = _questionRepository.Get(reply.SubjectId);
            if (question == null)
                throw new DefinedException(GetLocalizedResource(ErrorDefinitions.Question.QuestionNotExists));

            if (!_userRepository.Exists(userGuid))
                throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.UserNotFound));

            if (!new ReplyValidator(reply).Validate(out string errorCode))
                throw new DefinedException(GetLocalizedResource(errorCode));

            var answer = new Answer(reply.SubjectId, reply.ParentId, reply.Content, userGuid);
            question.AddAnswer(answer);

            _questionRepository.SaveChanges();

            return _questionRepository.GetAnswer(answer.Id);
        }

        public Answer UpdateAnswer(ReplyModel reply, Guid userGuid)
        {
            var answer = _questionRepository.GetAnswer(reply.Id);
            if (answer == null)
                throw new DefinedException(GetLocalizedResource(ErrorDefinitions.Answer.AnswerNotExists));

            if (answer.OwnerId != userGuid)
                throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.AccessDenied));

            if (!new ReplyValidator(reply).Validate(out string errorCode))
                throw new DefinedException(GetLocalizedResource(errorCode));

            answer.Update(reply.Content);
            _questionRepository.SaveChanges();

            return _questionRepository.GetAnswer(answer.Id);
        }
    }
}

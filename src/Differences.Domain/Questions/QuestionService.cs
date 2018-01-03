using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Differences.Common;
using Differences.Common.Resources;
using Differences.Domain.Models;
using Differences.Domain.Policies;
using Differences.Domain.Users;
using Differences.Domain.Validators;
using Differences.Interaction.DataTransferModels;
using Differences.Interaction.EntityModels;
using Differences.Interaction.Repositories;
using Microsoft.Extensions.Localization;

namespace Differences.Domain.Questions
{
    public partial class QuestionService : ServiceBase, IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;

        public QuestionService(
            IQuestionRepository questionRepository,
            IUserRepository userRepository,
            IUserContextService userContextService,
            IStringLocalizer<Errors> localizer)
            : base(userContextService, localizer)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
        }

        public QuestionModel GetQuestion(int questionId)
        {
            var query = from q in _questionRepository.GetAll()
                let o = q.Owner
                let answerCount = q.Answers.Count
                where q.Id == questionId
                select new QuestionModel(q)
                {
                    AnswerCount = answerCount
                };
            return query.First();
        }

        public QuestionModel AskQuestion(SubjectModel subject)
        {
            if (!new SubjectValidator(subject).Validate(out string errorCode))
                throw new DefinedException(GetLocalizedResource(errorCode));

            Question result = null;
            _questionRepository.UseTransaction(() =>
            {
                var user = _userRepository.Get(UserId);
                if (user == null)
                    throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.UserNotFound));

                result =
                    _questionRepository.Add(new Question(subject.Title, subject.Content, subject.CategoryId, UserId));
                _questionRepository.SaveChanges();

                user.UserScores.IncreaseContribution((int) ContributeTypeDefinition.NewQuestionAdded,
                    new NewQuestionContributionRule().IncreasingValue, result.Id);
                _questionRepository.SaveChanges();

            });

            return QuestionModel(result);
        }

        public QuestionModel UpdateQuestion(SubjectModel subject)
        {
            if (!new SubjectValidator(subject).Validate(out string errorCode))
                throw new DefinedException(GetLocalizedResource(errorCode));

            Question question = null;
            _questionRepository.UseTransaction(() =>
            {
                if (!_userRepository.Exists(UserId))
                    throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.UserNotFound));

                question = _questionRepository.Get(subject.Id);
                if (question == null)
                    throw new DefinedException(GetLocalizedResource(ErrorDefinitions.Question.QuestionNotExists));

                if (question.OwnerId != UserId)
                    throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.AccessDenied));

                question.Update(subject.Title, subject.Content, subject.CategoryId);
                _questionRepository.SaveChanges();
            });

            return QuestionModel(question);
        }

        private QuestionModel QuestionModel(Question question)
        {
            return (from q in _questionRepository.Find(x => x.Id == question.Id)
                let answerCount = q.Answers.Count
                select new QuestionModel(q)
                {
                    AnswerCount = answerCount
                }).First();
        }

        public IReadOnlyList<QuestionModel> GetQuestionsByCriteria(CriteriaModel criteria)
        {
            var query = from q in GetSearchQuery(criteria.CategoryId).Skip(criteria.Offset ?? 0).Take(criteria.Limit ?? 0)
                        let answerCount = q.Answers.Count
                        select new QuestionModel(q)
                        {
                            AnswerCount = answerCount
                        };
            return query.ToList();
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
    }
}

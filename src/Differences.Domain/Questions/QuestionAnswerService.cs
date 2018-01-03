using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Differences.Common;
using Differences.Domain.Models;
using Differences.Domain.Policies;
using Differences.Domain.Validators;
using Differences.Interaction.DataTransferModels;
using Differences.Interaction.EntityModels;

namespace Differences.Domain.Questions
{
    public partial class QuestionService
    {
        public IReadOnlyList<Answer> GetAnswersByQuestionId(int questionId)
        {
            var answers = _questionRepository.GetAnswers(questionId);
            return answers.OrderByDescending(x => x.CreateTime).ToList();
        }

        public Answer AddAnswer(ReplyModel reply)
        {
            if (!new ReplyValidator(reply).Validate(out string errorCode))
                throw new DefinedException(GetLocalizedResource(errorCode));

            Answer answer = null; ;
            _questionRepository.UseTransaction(() =>
            {
                var user = _userRepository.Get(UserId);
                if (user == null)
                    throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.UserNotFound));

                var question = _questionRepository.Get(reply.SubjectId);
                if (question == null)
                    throw new DefinedException(GetLocalizedResource(ErrorDefinitions.Question.QuestionNotExists));

                answer = new Answer(reply.SubjectId, reply.ParentId, reply.Content, UserId);
                question.AddAnswer(answer);

                _questionRepository.SaveChanges();

                user.UserScores.IncreaseContribution((int) ContributeTypeDefinition.NewAnswerAdded,
                    new NewReplyContributionRule().IncreasingValue, answer.Id);
                _questionRepository.SaveChanges();
            });

            return _questionRepository.GetAnswer(answer.Id);
        }

        public Answer UpdateAnswer(ReplyModel reply)
        {
            if (!new ReplyValidator(reply).Validate(out string errorCode))
                throw new DefinedException(GetLocalizedResource(errorCode));

            Answer answer = null;
            _questionRepository.UseTransaction(() =>
            {
                answer = _questionRepository.GetAnswer(reply.Id);
                if (answer == null)
                    throw new DefinedException(GetLocalizedResource(ErrorDefinitions.Answer.AnswerNotExists));

                if (answer.OwnerId != UserId)
                    throw new DefinedException(GetLocalizedResource(ErrorDefinitions.User.AccessDenied));

                answer.Update(reply.Content);
                _questionRepository.SaveChanges();
            });

            // Cannot like the answer provided by myself
            return _questionRepository.GetAnswer(answer.Id);
        }
    }
}

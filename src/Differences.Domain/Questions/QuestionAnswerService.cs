using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Differences.Common;
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

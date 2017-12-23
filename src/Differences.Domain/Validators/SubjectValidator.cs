using System;
using Differences.Common;
using Differences.Interaction.DataTransferModels;

namespace Differences.Domain.Validators
{
    public class SubjectValidator : IValidator
    {
        private readonly SubjectModel _model;
        public SubjectValidator(SubjectModel model)
        {
            _model = model;
        }

        public bool Validate(out string errorCode)
        {
            errorCode = string.Empty;
            if (_model.Title.Length < 5)
            {
                errorCode = ErrorDefinitions.Question.TitleLengthTooShort;
                return false;
            }

            if (_model.Title.Length > 60)
            {
                errorCode = ErrorDefinitions.Question.TitleLengthExceeding;
                return false;
            }

            if (_model.Content.Length < 5)
            {
                errorCode = ErrorDefinitions.Question.ContentLengthTooShort;
                return false;
            }

            if (_model.Content.Length > 200)
            {
                errorCode = ErrorDefinitions.Question.ContentLengthExceeding;
            }
            return true;
        }
    }
}

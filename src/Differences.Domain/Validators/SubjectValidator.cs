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
            if (_model.Title.Length > 100)
            {
                errorCode = ErrorDefinitions.Question.TitleLengthExceeding;
                return false;
            }

            if (_model.Content.Length > 4000)
            {
                errorCode = ErrorDefinitions.Question.ContentLengthExceeding;
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Differences.Common;
using Differences.Interaction.DataTransferModels;

namespace Differences.Domain.Validators
{
    public class ReplyValidator : IValidator
    {
        private readonly ReplyModel _model;

        public ReplyValidator(ReplyModel model)
        {
            _model = model;
        }

        public bool Validate(out string errorCode)
        {
            errorCode = string.Empty;
            if (_model.Content.Length < 5)
            {
                errorCode = ErrorDefinitions.Answer.ContentLengthTooShort;
                return false;
            }

            //if (_model.Content.Length > 60)
            //{
            //    errorCode = ErrorDefinitions.Answer.ContentLengthExceeding;
            //    return false;
            //}

            return true;
        }
    }
}

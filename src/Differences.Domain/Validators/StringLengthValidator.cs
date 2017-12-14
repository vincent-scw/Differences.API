using System;
namespace Differences.Domain.Validators
{
    public class StringLengthValidator : IValidator
    {
        private readonly int _length;
        public StringLengthValidator(int length)
        {
            _length = length;
        }

        public bool Validate(out string errorCode)
        {
            throw new NotImplementedException();
        }
    }
}

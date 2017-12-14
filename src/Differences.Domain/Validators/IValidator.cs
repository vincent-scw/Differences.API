using System;
namespace Differences.Domain.Validators
{
    public interface IValidator
    {
        bool Validate(out string errorCode);
    }
}

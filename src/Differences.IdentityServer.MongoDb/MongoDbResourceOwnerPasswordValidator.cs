using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Differences.IdentityServer.MongoDb
{
    public class MongoDbResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IRepository _repository;

        public MongoDbResourceOwnerPasswordValidator(IRepository repository)
        {
            _repository = repository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (_repository.ValidatePassword(context.UserName, context.Password))
            {
                return Task.FromResult(new GrantValidationResult(context.UserName, "password"));
            }

            return Task.FromResult(new GrantValidationResult(TokenRequestErrors.InvalidRequest));
        }
    }
}

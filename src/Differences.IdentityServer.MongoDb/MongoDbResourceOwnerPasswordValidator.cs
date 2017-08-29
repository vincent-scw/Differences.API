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
                context.Result = new GrantValidationResult(context.UserName, "password", System.DateTime.Now);
            else
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid_username_or_password");

            return Task.FromResult(0);
        }
    }
}

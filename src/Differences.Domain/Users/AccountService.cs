using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Differences.Common;
using Differences.Common.Resources;
using Differences.Domain.Models;
using Differences.Interaction.Repositories;
using Differences.OAuth2Provider;
using Microsoft.Extensions.Localization;

namespace Differences.Domain.Users
{
    public class AccountService : ServiceBase, IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly OAuth2ProviderFactory _providerFactory;

        public AccountService(IUserRepository userRepository,
            OAuth2ProviderFactory providerFactory,
            IUserContextService userContextService, IStringLocalizer<Errors> localizer) 
            : base(userContextService, localizer)
        {
            _userRepository = userRepository;
            _providerFactory = providerFactory;
        }

        public UserWithTokenModel GetAuthResponse(string accountType, string code)
        {
            var type = Enum.Parse<AccountType>(accountType, true);
            var provider = _providerFactory.GetProvider(type);

            try
            {
                var response = provider.GetAuthResponseAsync(code).Result;
            }
            catch (InvalidOperationException ioe)
            {
                throw new DefinedException(GetLocalizedResource(ioe.Message));
            }

            return null;
        }
    }
}

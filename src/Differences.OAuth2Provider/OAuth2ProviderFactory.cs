using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Differences.OAuth2Provider.Configuration;

namespace Differences.OAuth2Provider
{
    public class OAuth2ProviderFactory
    {
        private readonly OpenIdAuthorization _openIdAuthorization;

        public OAuth2ProviderFactory(IOptions<OpenIdAuthorization> options)
        {
            _openIdAuthorization = options.Value;
        }

        public IAuthProvider GetProvider(AccountType type)
        {
            switch (type)
            {
                case AccountType.LinkedIn:
                    return new LinkedInProvider(_openIdAuthorization.LinkedInConfig);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

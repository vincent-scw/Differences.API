using System;
using System.Collections.Generic;
using System.Text;
using Differences.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Differences.Domain
{
    public abstract class ServiceBase
    {
        private readonly IStringLocalizer<Errors> _localizer;

        protected IUserContextService UserContextService { get; private set; }
        protected Guid UserId { get; private set; }

        public ServiceBase(IUserContextService userContextService, 
            IStringLocalizer<Errors> localizer)
        {
            _localizer = localizer;
            UserContextService = userContextService;
            var userInfo = UserContextService.GetUserInfo();
            if (userInfo != null)
                UserId = userInfo.Id;
        }

        protected string GetLocalizedResource(string name)
        {
            return _localizer.GetString(name);
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Differences.Common;
using Differences.Common.Extensions;

namespace Differences.Domain
{
    public class HttpUserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpUserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserInfo GetUserInfo()
        {
            return _httpContextAccessor.HttpContext.User.GetUserInfo();
        }
    }
}

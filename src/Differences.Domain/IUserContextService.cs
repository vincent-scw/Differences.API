using System;
using System.Collections.Generic;
using System.Text;
using Differences.Common;

namespace Differences.Domain
{
    public interface IUserContextService
    {
        UserInfo GetUserInfo();
    }
}

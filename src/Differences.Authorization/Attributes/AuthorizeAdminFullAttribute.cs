using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Differences.Authorization.Attributes
{
    public class AuthorizeAdminFullAttribute : AuthorizeAttribute
    {
        public AuthorizeAdminFullAttribute()
            : base(Policies.AdminFullControll)
        {

        }
    }
}

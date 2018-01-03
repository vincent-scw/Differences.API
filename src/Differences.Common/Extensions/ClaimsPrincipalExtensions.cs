using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.VisualBasic;

namespace Differences.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="ClaimsPrincipal"/>.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get the user id who is performing action on the system.
        /// If impersonation state, returns the impersonated user id; returns the sub user id.
        /// </summary>
        /// <param name="user">The user principal. (asp.net core user)</param>
        /// <returns>The id for the transaction user.</returns>
        public static UserInfo GetUserInfo(this ClaimsPrincipal user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return new UserInfo(user);
        }
    }
}

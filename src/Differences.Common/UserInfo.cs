using System;
using System.Security.Claims;

namespace Differences.Common
{
    public class UserInfo
    {
        public UserInfo(ClaimsPrincipal claimsPrincipal)
        {
            var userGuidString = claimsPrincipal.FindFirst(x => x.Type.Equals(ClaimTypes.Sid)).Value;
            Guid.TryParse(userGuidString, out Guid userGuid);
            this.Id = userGuid;

            this.DisplayName = claimsPrincipal.FindFirst(x => x.Type.Equals(ClaimTypes.Name)).Value;
            this.Email = claimsPrincipal.FindFirst(x => x.Type.Equals(ClaimTypes.Email))?.Value;
            this.Role = claimsPrincipal.FindFirst(x => x.Type.Equals(ClaimTypes.Role))?.Value;
        }
        
        public string DisplayName { get; }
        public string Email { get; }
        public string Role { get; }
        public Guid Id { get; }
    }
}

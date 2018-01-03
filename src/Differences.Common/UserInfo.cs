using System;
using System.Security.Claims;

namespace Differences.Common
{
    public class UserInfo
    {
        private const string Claims_ObjectId = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        private const string Claims_Name = "name";
        private const string Claims_Emails = "emails";

        public UserInfo(ClaimsPrincipal claimsPrincipal)
        {
            var userGuidString = claimsPrincipal.FindFirst(x => x.Type.Equals(Claims_ObjectId)).Value;
            Guid.TryParse(userGuidString, out Guid userGuid);
            this.Id = userGuid;

            this.DisplayName = claimsPrincipal.FindFirst(x => x.Type.Equals(Claims_Name)).Value;
            this.Email = claimsPrincipal.FindFirst(x => x.Type.Equals(Claims_Emails)).Value; // Only 1 email is allowed
        }
        
        public string DisplayName { get; }
        public string Email { get; }
        public string AvatarUrl { get; }
        public Guid Id { get; }
    }
}

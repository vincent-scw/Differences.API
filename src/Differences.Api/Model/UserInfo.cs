using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Differences.Api.Model
{
    public class UserInfo
    {
        private const string Claims_ObjectId = "aud";
        private const string Claims_Name = "name";
        private const string Claims_Emails = "emails";

        public UserInfo(ClaimsPrincipal claimsPrincipal)
        {
            var userGuidString = claimsPrincipal.FindFirst(x => x.Type.Equals(Claims_ObjectId)).Value;
            Guid.TryParse(userGuidString, out Guid userGuid);
            this.GlobalId = userGuid;

            this.DisplayName = claimsPrincipal.FindFirst(x => x.Type.Equals(Claims_Name)).Value;
            this.Email = claimsPrincipal.FindFirst(x => x.Type.Equals(Claims_Emails)).Value; // Only 1 email is allowed
        }

        public long Id { get; }
        public string DisplayName { get; }
        public string Email { get; }
        public string AvatarUrl { get; }
        public Guid GlobalId { get; }
    }
}

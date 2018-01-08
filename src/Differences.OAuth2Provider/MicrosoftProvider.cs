using System;
using System.Collections.Generic;
using System.Text;
using Differences.OAuth2Provider.Configuration;
using Newtonsoft.Json.Linq;

namespace Differences.OAuth2Provider
{
    public class MicrosoftProvider : AuthProviderBase
    {
        public MicrosoftProvider(OAuth2Config config) : base(config)
        {
        }

        protected override string AccessTokenUrl => "https://login.live.com/oauth20_token.srf";
        protected override string UserInfoUrl => "https://apis.live.net/v5.0/me";
        protected override int GetUserInfoMethod => 1;
        protected override UserInfo GetUserInfo(JObject obj)
        {
            var userInfo = new UserInfo
            {
                Id = (string)obj["id"],
                DisplayName = (string)obj["name"],
            };
            userInfo.AvatarUrl = $"https://apis.live.net/v5.0/{userInfo.Id}/picture";

            return userInfo;
        }
    }
}

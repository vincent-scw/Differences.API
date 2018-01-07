using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Differences.Common;
using Differences.OAuth2Provider.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Differences.OAuth2Provider
{
    public class LinkedInProvider : IAuthProvider
    {
        private LinkedInConfig _config;

        public LinkedInProvider(LinkedInConfig config)
        {
            _config = config;
        }

        public AuthResponse GetAuthResponse(string code)
        {
            var accessToken = GetAccessToken(code);
            if (string.IsNullOrEmpty(accessToken))
                throw new InvalidOperationException(ErrorDefinitions.User.AuthCodeInvalid);

            var userInfo = GetUserInfo(accessToken);
            return new AuthResponse {AccessToken = accessToken, UserInfo = userInfo};
        }

        private string GetAccessToken(string code)
        {
            var tokenUrl = "https://www.linkedin.com/uas/oauth2/accessToken";
            var client = new HttpClient();
            var formContent = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    {"grant_type", "authorization_code"},
                    {"client_id", _config.ClientId},
                    {"client_secret", _config.ClientSecret},
                    {"code", code},
                    {"redirect_uri", _config.RedirectUrl}
                });

            var response = client.PostAsync(tokenUrl, formContent).Result;
            var contentStr = response.Content.ReadAsStringAsync().Result;
            return (string)(JObject.Parse(contentStr)["access_token"]);
        }

        private UserInfo GetUserInfo(string accessToken)
        {
            var userInfoUrl =
                "https://api.linkedin.com/v1/people/~:(id,formatted-name,picture-url)?format=json";
            var client = new HttpClient();
            var message = new HttpRequestMessage(HttpMethod.Get, new Uri(userInfoUrl));
            message.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = client.SendAsync(message).Result;
            var contentStr = response.Content.ReadAsStringAsync().Result;
            var jObject = JObject.Parse(contentStr);
            return new UserInfo
            {
                Id = (string) jObject["id"],
                DisplayName = (string) jObject["formattedName"],
                AvatarUrl = (string) jObject["pictureUrl"]
            };
        }
    }
}

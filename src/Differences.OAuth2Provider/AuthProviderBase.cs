using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Differences.Common;
using Differences.OAuth2Provider.Configuration;
using Newtonsoft.Json.Linq;

namespace Differences.OAuth2Provider
{
    public abstract class AuthProviderBase : IAuthProvider
    {
        private OAuth2Config _config;

        public AuthProviderBase(OAuth2Config config)
        {
            _config = config;
        }

        public AuthResponse GetAuthResponse(string code)
        {
            var accessToken = GetAccessToken(code);
            if (string.IsNullOrEmpty(accessToken))
                throw new InvalidOperationException(ErrorDefinitions.User.AuthCodeInvalid);

            var userInfo = GetUserInfo(GetUserInfo(accessToken));
            return new AuthResponse { AccessToken = accessToken, UserInfo = userInfo };
        }

        protected abstract string AccessTokenUrl { get; }
        protected abstract string UserInfoUrl { get; }
        /// <summary>
        /// // 0 header, 1 querystring
        /// </summary>
        protected abstract int GetUserInfoMethod { get; } 
        protected abstract UserInfo GetUserInfo(JObject obj);

        protected virtual string GetAccessToken(string code)
        {
            var tokenUrl = AccessTokenUrl;
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

        private JObject GetUserInfo(string accessToken)
        {
            var userInfoUrl = UserInfoUrl;
            var client = new HttpClient();
            var message = new HttpRequestMessage(HttpMethod.Get, new Uri(userInfoUrl));
            if (GetUserInfoMethod == 0)
            {
                message.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
            else
            {
                message.RequestUri = new Uri($"{userInfoUrl}?access_token={accessToken}");
            }

            var response = client.SendAsync(message).Result;
            var contentStr = response.Content.ReadAsStringAsync().Result;
            return JObject.Parse(contentStr);
        }
    }
}

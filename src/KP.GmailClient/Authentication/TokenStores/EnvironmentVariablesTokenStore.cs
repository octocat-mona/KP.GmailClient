using System;
using System.Globalization;
using System.Threading.Tasks;
using KP.GmailClient.Extensions;
using KP.GmailClient.Models;

namespace KP.GmailClient.Authentication.TokenStores
{
    public class EnvironmentVariablesTokenStore : ITokenStore
    {
        private const string DateFormat = "O";
        private readonly string _keyPrefix;
        private readonly EnvironmentVariableTarget _target;
        private OAuth2Token _cachedToken;

        public string AccessTokenKeyName { get; set; } = "access_token";
        public string RefreshTokenKeyName { get; set; } = "refresh_token";
        public string ExpiryDateTokenKeyName { get; set; } = "expiry_date";

        /// <summary>Token store which stores the tokens as environment variables.</summary>
        /// <param name="keyPrefix">
        /// An optional prefix for the <see cref="AccessTokenKeyName"/>, <see cref="RefreshTokenKeyName"/> and <see cref="ExpiryDateTokenKeyName"/> variables.
        /// </param>
        /// <param name="target"></param>
        public EnvironmentVariablesTokenStore(string keyPrefix = "", EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
        {
            _keyPrefix = keyPrefix;
            _target = target;
        }

        public Task<OAuth2Token> GetTokenAsync()
        {
            if (!_cachedToken.IsExpired())
            {
                return Task.FromResult(_cachedToken);
            }

            string GetVariable(string key)
            {
                return Environment.GetEnvironmentVariable($"{_keyPrefix}{key}", _target);
            }

            string refreshToken = GetVariable(RefreshTokenKeyName);
            string accessToken = GetVariable(AccessTokenKeyName);
            string expiryDate = GetVariable(ExpiryDateTokenKeyName);

            if (string.IsNullOrWhiteSpace(refreshToken) && string.IsNullOrWhiteSpace(accessToken))
            {
                throw new Exception("No environment variables found for both refresh and access token");
            }


            bool isValidDate = DateTimeOffset.TryParseExact(expiryDate, DateFormat, null, DateTimeStyles.None, out var date);

            _cachedToken = new OAuth2Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpirationDate = isValidDate ? date : DateTimeOffset.MinValue
            };

            return Task.FromResult(_cachedToken);
        }

        public Task StoreTokenAsync(OAuth2Token token)
        {
            // Only write to disk when one of the 2 tokens changed
            if (_cachedToken != null && _cachedToken.AccessToken == token.AccessToken && _cachedToken.RefreshToken == token.RefreshToken)
            {
                return Task.FromResult(0);
            }

            void SetVariable(string key, string value) => Environment.SetEnvironmentVariable($"{_keyPrefix}{key}", value, _target);

            SetVariable(RefreshTokenKeyName, token.RefreshToken);
            SetVariable(AccessTokenKeyName, token.AccessToken);
            SetVariable(ExpiryDateTokenKeyName, token.ExpirationDate.ToString(DateFormat));

            _cachedToken = token;
            return Task.FromResult(0);
        }
    }
}

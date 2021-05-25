using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using KP.GmailClient.Extensions;
using KP.GmailClient.Models;

namespace KP.GmailClient.Authentication.TokenStores
{
    /// <summary>Token store which stores the token as json file on disk.</summary>
    public class FileTokenStore : ITokenStore
    {
        private readonly string _tokenFile;
        private OAuth2Token _cachedToken;

        /// <summary>Token store which stores the token as json file on disk.</summary>
        /// <param name="tokenFile">The full file path including extension.</param>
        public FileTokenStore(string tokenFile)
        {
            _tokenFile = tokenFile ?? throw new ArgumentNullException(nameof(tokenFile));
        }

        public async Task<OAuth2Token> GetTokenAsync()
        {
            if (!_cachedToken.IsExpired())
            {
                return _cachedToken;
            }

            using (var stream = File.OpenRead(_tokenFile))
            {
                _cachedToken = await JsonSerializer.DeserializeAsync<OAuth2Token>(stream);
                return _cachedToken;
            }
        }

        public async Task StoreTokenAsync(OAuth2Token token)
        {
            // Only write to disk when one of the 2 tokens changed
            if (_cachedToken != null && _cachedToken.AccessToken == token.AccessToken && _cachedToken.RefreshToken == token.RefreshToken)
            {
                return;
            }

            using (var stream = File.OpenWrite(_tokenFile))
            {
                await JsonSerializer.SerializeAsync(stream, token);
            }

            _cachedToken = token;
        }
    }
}

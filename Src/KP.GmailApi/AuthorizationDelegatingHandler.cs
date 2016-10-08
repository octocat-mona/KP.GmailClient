using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using KP.GmailApi.Models;
using Newtonsoft.Json;

namespace KP.GmailApi
{
    internal class AuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly ITokenStore _tokenStore;
        //private readonly OAuth2TokenManager _tokenManager;
        /// <summary>
        /// The Google Authorization server URL used to authenticate.
        /// </summary>
        public const string AuthorizationServerUrl = "https://www.googleapis.com/oauth2/v3/token";// "https://accounts.google.com/o/oauth2/token";

        //private static readonly ConcurrentDictionary<string, OAuth2Token> Tokens = new ConcurrentDictionary<string, OAuth2Token>();
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        private TokenData _tokenData;

        public AuthorizationDelegatingHandler(ITokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token;
            try
            {
                await _semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);
                token = await GetTokenAsync();
            }
            finally
            {
                _semaphoreSlim.Release();
            }

            //string token = await /*_tokenManager.*/GetTokenAsync().ConfigureAwait(false);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Get an access token. Will return an valid existing token or retrieves a new one if expired.
        /// </summary>
        /// <returns>An access token</returns>
        internal async Task<string> GetTokenAsync()
        {
            // Retrieve clientId and clientSecret once
            if (_tokenData == null)
            {
                _tokenData = _tokenStore.Load();
                if (_tokenData == null)
                {
                    throw new Exception("Can't load token data");
                }
            }

            // Check if there's a stored token already
            var token = await _tokenStore.LoadAsync();
            if (token != null)
            {
                // Use access token is still valid
                if (DateTime.UtcNow < token.ExpirationDate)
                {
                    return token.AccessToken;
                }

                _tokenData.RefreshToken = token.RefreshToken;
            }

            // Access token not valid (anymore), request new one
            const string url = AuthorizationServerUrl;
            string content = string.Concat(
                "refresh_token=", HttpUtility.UrlEncode(_tokenData.RefreshToken),
                "&client_id=", HttpUtility.UrlEncode(_tokenData.ClientId),
                "&client_secret=", HttpUtility.UrlEncode(_tokenData.ClientSecret),
                "&grant_type=refresh_token"
                );

            var stringContent = new StringContent(content);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            var result = await client.PostAsync(url, stringContent).ConfigureAwait(false);
            string json = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            result.EnsureSuccessStatusCode();

            string currentRefreshToken = _tokenData.RefreshToken;
            token = JsonConvert.DeserializeObject<OAuth2Token>(json);

            if (string.IsNullOrWhiteSpace(token.RefreshToken))
            {
                token.RefreshToken = currentRefreshToken;
            }

            token.ExpirationDate = DateTime.UtcNow.AddSeconds(token.ExpiresIn);
            await _tokenStore.StoreAsync(token);

            return token.AccessToken;
        }
    }
}

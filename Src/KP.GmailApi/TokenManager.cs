using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using KP.GmailApi.Models;
using Newtonsoft.Json;

namespace KP.GmailApi
{
    /// <summary>
    /// A manager which retrieves and stores a token of a client ID.
    /// </summary>
    public class TokenManager
    {
        /// <summary>
        /// The Google Authorization server URL used to authenticate.
        /// </summary>
        public const string AuthorizationServerUrl = "https://www.googleapis.com/oauth2/v3/token";// "https://accounts.google.com/o/oauth2/token";

        private static readonly ConcurrentDictionary<string, Oauth2Token> Tokens = new ConcurrentDictionary<string, Oauth2Token>();
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _tokenFile;
        private Oauth2Token _token;

        /// <summary>
        /// A manager which retrieves and stores a token of a client ID.
        /// </summary>
        /// <param name="clientId">The client ID of your project listed in the Google Developer Console</param>
        /// <param name="clientSecret">The client secret of your project listed in the Google Developer Console</param>
        public TokenManager(string clientId, string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException("clientId");

            _clientId = clientId;
            _clientSecret = clientSecret;

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            _tokenFile = Path.Combine(appData, "GmailService\\", clientId.GetValidFilename() + ".json");
            _token = Tokens.GetOrAdd(_tokenFile, new Oauth2Token());
        }

        /// <summary>
        /// Get an access token. Will return an valid existing token or retrieves a new one if expired.
        /// </summary>
        /// <returns>An access token</returns>
        internal string GetToken()
        {
            lock (_token)
            {
                if (_token.RefreshToken == null)
                {
                    string jsonText = File.ReadAllText(_tokenFile);
                    _token = JsonConvert.DeserializeObject<Oauth2Token>(jsonText);
                }

                // Check if token is still valid
                if (DateTime.UtcNow < _token.ExpirationDate)
                    return _token.AccessToken;

                const string url = AuthorizationServerUrl;
                string content = string.Concat(
                    "refresh_token=", HttpUtility.UrlEncode(_token.RefreshToken),
                    "&client_id=", HttpUtility.UrlEncode(_clientId),
                    "&client_secret=", HttpUtility.UrlEncode(_clientSecret),
                    "&grant_type=", HttpUtility.UrlEncode("refresh_token")
                    );

                var stringContent = new StringContent(content);
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var client = new HttpClient();
                var result = client.PostAsync(url, stringContent).Result;
                string json = result.Content.ReadAsStringAsync().Result;

                result.EnsureSuccessStatusCode();

                string currentRefreshToken = _token.RefreshToken;
                _token = JsonConvert.DeserializeObject<Oauth2Token>(json);
                _token.RefreshToken = currentRefreshToken;
                _token.ExpirationDate = DateTime.UtcNow.AddSeconds(_token.ExpiresIn);

                WriteToFile();

                return _token.AccessToken;
            }
        }

        private void WriteToFile()
        {
            // Will create the directory/directories if it doesn't exist
            var tokenFile = new FileInfo(_tokenFile);
            if (tokenFile.Directory != null && !tokenFile.Directory.Exists)
                tokenFile.Directory.Create();

            string tokenString = JsonConvert.SerializeObject(_token);
            File.WriteAllText(_tokenFile, tokenString);
        }

        /// <summary>
        /// Set the refresh token. This is only required for the first request.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="force"></param>
        public void Setup(string refreshToken, bool force)
        {
            if (!force && File.Exists(_tokenFile))
                return;

            var token = new Oauth2Token
            {
                TokenType = "Bearer",
                RefreshToken = refreshToken
            };

            lock (Tokens)
            {
                _token = Tokens.AddOrUpdate(_tokenFile, token, (key, oldvalue) => token);
                WriteToFile();
            }
        }

        /// <summary>
        /// Checks if the token based on the ClientId has been saved.
        /// </summary>
        /// <returns></returns>
        public bool HasTokenSetup()
        {
            return new FileInfo(_tokenFile).Exists;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using KP.GmailClient.Authentication.Dtos;
using KP.GmailClient.Models;

namespace KP.GmailClient.Authentication.TokenClients
{
    /// <summary><inheritdoc cref="ITokenClient"/></summary>
    public class TokenClient : ITokenClient
    {
        private readonly OAuth2ClientCredentials _credentials;
        private readonly HttpClient _httpClient;

        public TokenClient(OAuth2ClientCredentials credentials)
        {
            _credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            _httpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
        }

        /// <summary></summary>
        /// <param name="clientCredentialsFile">The client credentials file as downloaded from the Google Cloud Console.</param>
        public static async Task<TokenClient> CreateAsync(string clientCredentialsFile)
        {
            using (var stream = File.OpenRead(clientCredentialsFile))
            {
                var credentials = (await JsonSerializer.DeserializeAsync<OAuth2ClientCredentialsWrapper>(stream))?.Credentials
                                  ?? throw new ArgumentException("Invalid credentials file", nameof(clientCredentialsFile));

                return new TokenClient(credentials);
            }
        }

        public async Task<OAuth2Token> GetTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                { "client_id", _credentials.ClientId },
                { "client_secret", _credentials.ClientSecret },
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            };

            var token = await GetTokenAsync(parameters, cancellationToken);
            // Google does not return the current or new refresh token
            token.RefreshToken ??= refreshToken;

            return token;
        }

        public async Task<OAuth2Token> ExchangeCodeAsync(string redirectUri, string code, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                { "client_id", _credentials.ClientId },
                { "client_secret", _credentials.ClientSecret },
                { "code", code },
                { "grant_type", "authorization_code" },
                { "redirect_uri", redirectUri }
            };

            var token = await GetTokenAsync(parameters, cancellationToken);
            return token;
        }

        private async Task<OAuth2Token> GetTokenAsync(Dictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsync(_credentials.TokenUri, new FormUrlEncodedContent(parameters), cancellationToken);
            response.EnsureSuccessStatusCode();

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var token = await JsonSerializer.DeserializeAsync<OAuth2Token>(stream, cancellationToken: cancellationToken)
                    ?? throw new JsonException("Invalid JSON token response");
                token.ExpirationDate = DateTimeOffset.UtcNow.AddSeconds(token.ExpiresIn);
                return token;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

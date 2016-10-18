using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Jose;
using KP.GmailApi.Common;
using KP.GmailApi.Models;
using Newtonsoft.Json;

namespace KP.GmailApi
{
    internal class AuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly string _emailAddress;
        private readonly string _scopes;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly HttpClient _client;
        private readonly JsonSerializer _jsonSerializer = new JsonSerializer();
        private readonly ServiceAccountCredential _accountCredential;
        private OAuth2Token _token;


        public AuthorizationDelegatingHandler(ServiceAccountCredential accountCredential, string emailAddress, string scopes)
        {
            _accountCredential = accountCredential;
            _emailAddress = emailAddress;
            _scopes = scopes;
            _client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await GetTokenSynchronized(cancellationToken, true);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await base.SendAsync(request, cancellationToken);
            if (responseMessage.StatusCode != HttpStatusCode.Unauthorized)
            {
                return responseMessage;
            }

            // Request returned 401, try once again with new token
            token = await GetTokenSynchronized(cancellationToken, false);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<string> GetTokenSynchronized(CancellationToken cancellationToken, bool useExpiryCheck)
        {
            try
            {
                await _semaphoreSlim.WaitAsync(cancellationToken);
                return await GetTokenAsync(cancellationToken, useExpiryCheck);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        /// <summary>
        /// Get an access token. Will return an valid existing token or retrieves a new one if expired.
        /// Note that the token could still be invalid when revoked remotely or because of clock skew for example.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="useExpiryCheck">True to return an existing token if not expired, false to force retrieval of new token</param>
        /// <returns>An access token</returns>
        private async Task<string> GetTokenAsync(CancellationToken cancellationToken, bool useExpiryCheck)
        {
            // Use access token if still valid
            if (useExpiryCheck && _token != null && DateTime.UtcNow < _token.ExpirationDate)
            {
                return _token.AccessToken;
            }

            // Access token not valid (anymore), request new one
            var rsaCryptoServiceProvider = Opensslkey.GetRsaFromPemKey(_accountCredential.PrivateKey);

            var payload = new Dictionary<string, object>
            {
                { "iss", _accountCredential.ClientEmail },
                { "scope", _scopes },
                { "aud", _accountCredential.TokenUri },
                { "sub", _emailAddress },
                { "iat", DateTime.UtcNow.ToUnixTime() },
                { "exp", DateTime.UtcNow.AddHours(1).ToUnixTime() }
            };

            string jwt = JWT.Encode(payload, rsaCryptoServiceProvider, JwsAlgorithm.RS256);
            var content = new Dictionary<string, string>
            {
                { "assertion", jwt },
                { "grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer" }
            };

            var responseMessage = await _client.PostAsync(_accountCredential.TokenUri, new FormUrlEncodedContent(content), cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                //TODO: parse error response
                string contentString = await responseMessage.Content.ReadAsStringAsync();
                GmailException ex = ErrorResponseParser.Parse(responseMessage.StatusCode, contentString);
                throw ex;
            }

            using (var stream = await responseMessage.Content.ReadAsStreamAsync())
            using (var streamReader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                _token = _jsonSerializer.Deserialize<OAuth2Token>(jsonTextReader);
            }

            _token.ExpirationDate = DateTime.UtcNow.AddSeconds(_token.ExpiresIn);
            return _token.AccessToken;
        }
    }
}

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using KP.GmailClient.Authentication.TokenClients;
using KP.GmailClient.Authentication.TokenStores;
using KP.GmailClient.Extensions;

namespace KP.GmailClient
{
    internal class AuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly ITokenClient _tokenClient;
        private readonly ITokenStore _tokenStore;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public AuthorizationDelegatingHandler(ITokenClient tokenClient, ITokenStore tokenStore)
        {
            _tokenClient = tokenClient;
            _tokenStore = tokenStore;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            async Task<HttpResponseMessage> SendRequestAsync()
            {
                string token = await GetTokenSynchronized(cancellationToken);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await base.SendAsync(request, cancellationToken);
            }

            var responseMessage = await SendRequestAsync();
            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Request returned 401, try once again with new token
                return await SendRequestAsync();
            }

            return responseMessage;
        }

        private async Task<string> GetTokenSynchronized(CancellationToken cancellationToken)
        {
            try
            {
                await _semaphoreSlim.WaitAsync(cancellationToken);

                var existingToken = await _tokenStore.GetTokenAsync();
                if (!existingToken.IsExpired())
                {
                    return existingToken.AccessToken;
                }

                var token = await _tokenClient.GetTokenAsync(existingToken.RefreshToken, cancellationToken);
                await _tokenStore.StoreTokenAsync(token);
                return token.AccessToken;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _semaphoreSlim.Dispose();
            base.Dispose(disposing);
        }
    }
}

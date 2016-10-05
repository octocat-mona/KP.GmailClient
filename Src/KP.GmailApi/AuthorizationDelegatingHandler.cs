using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using KP.GmailApi.Managers;

namespace KP.GmailApi
{
    internal class AuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly OAuth2TokenManager _tokenManager;

        public AuthorizationDelegatingHandler(OAuth2TokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = await _tokenManager.GetTokenAsync().ConfigureAwait(false);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

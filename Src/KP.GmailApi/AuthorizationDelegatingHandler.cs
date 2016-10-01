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

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = _tokenManager.GetToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return base.SendAsync(request, cancellationToken);
        }
    }
}

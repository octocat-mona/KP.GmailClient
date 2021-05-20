using System.Threading.Tasks;
using KP.GmailClient.Models;

namespace KP.GmailClient.Authentication.TokenStores
{
    public class InMemoryTokenStore : ITokenStore
    {
        private OAuth2Token _token;

        public InMemoryTokenStore(string refreshToken)
        {
            _token = new OAuth2Token { RefreshToken = refreshToken };
        }

        public InMemoryTokenStore(OAuth2Token token)
        {
            _token = token;
        }

        public Task<OAuth2Token> GetTokenAsync()
        {
            return Task.FromResult(_token);
        }

        public Task StoreTokenAsync(OAuth2Token token)
        {
            _token = token;
            return Task.FromResult(0);
        }
    }
}

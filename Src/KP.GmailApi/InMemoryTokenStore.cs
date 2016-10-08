using System.Threading.Tasks;
using KP.GmailApi.Models;

namespace KP.GmailApi
{
    public class InMemoryTokenStore : ITokenStore
    {
        private readonly TokenData _tokenData;
        private OAuth2Token _token;

        public InMemoryTokenStore(string clientId, string clientSecret, string refreshToken)
        {
            _tokenData = new TokenData { ClientId = clientId, ClientSecret = clientSecret, RefreshToken = refreshToken };
        }

        public TokenData Load()
        {
            return _tokenData;
        }

        public async Task<OAuth2Token> LoadAsync()
        {
            return await Task.FromResult(_token);
        }

        public async Task StoreAsync(OAuth2Token token)
        {
            _token = token;
            await Task.FromResult(0);
        }
    }
}
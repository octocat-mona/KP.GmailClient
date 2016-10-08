using System.Threading.Tasks;
using KP.GmailApi.Models;

namespace KP.GmailApi
{
    public interface ITokenStore
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TokenData Load();
        Task<OAuth2Token> LoadAsync();
        Task StoreAsync(OAuth2Token token);
    }

    public class TokenData
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RefreshToken { get; set; }
    }
}
using System;
using System.Threading.Tasks;
using KP.GmailClient.Models;

namespace KP.GmailClient.Authentication.TokenStores
{
    /// <summary>Manages storage of OAuth tokens.</summary>
    public interface ITokenStore
    {
        /// <summary>Retrieve the stored token.</summary>
        /// <exception cref="Exception">No token was stored.</exception>
        Task<OAuth2Token> GetTokenAsync();

        /// <summary>Store a new token.</summary>
        Task StoreTokenAsync(OAuth2Token token);
    }
}

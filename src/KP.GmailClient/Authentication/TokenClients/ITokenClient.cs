using System;
using System.Threading;
using System.Threading.Tasks;
using KP.GmailClient.Models;

namespace KP.GmailClient.Authentication.TokenClients
{
    /// <summary>A client which can retrieve OAuth tokens.</summary>
    public interface ITokenClient : IDisposable
    {
        /// <summary>Get a new access token using the refresh token.</summary>
        /// <param name="refreshToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="OAuth2Token"/>.</returns>
        Task<OAuth2Token> GetTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

        /// <summary>Exchange the code for a token.</summary>
        /// <param name="redirectUri"></param>
        /// <param name="code"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="OAuth2Token"/>.</returns>
        Task<OAuth2Token> ExchangeCodeAsync(string redirectUri, string code, CancellationToken cancellationToken = default);
    }
}
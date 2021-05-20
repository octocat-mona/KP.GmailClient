using System;
using KP.GmailClient.Models;

namespace KP.GmailClient.Extensions
{
    internal static class OAuth2TokenExtensions
    {
        internal static bool IsExpired(this OAuth2Token token)
        {
            return string.IsNullOrWhiteSpace(token?.AccessToken) || DateTimeOffset.UtcNow >= token.ExpirationDate;
        }
    }
}

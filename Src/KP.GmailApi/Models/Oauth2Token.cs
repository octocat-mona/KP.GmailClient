using System;
using Newtonsoft.Json;

namespace KP.GmailApi.Models
{
    /// <summary>
    /// An OAuth 2.0 token.
    /// </summary>
    public class OAuth2Token
    {
        /// <summary>
        /// A string representing an authorization issued to the client.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// A credential used to obtain access tokens.
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Provides the client with the information required to successfully utilize the access token
        /// to make a protected resource request (along with type-specific attributes), for example: Bearer.
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// The lifetime in seconds of the access token.
        /// </summary>
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        /// <summary>
        /// The expiration date of the access token.
        /// </summary>
        [JsonProperty]
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// A string with the values of the properties from this <see cref="OAuth2Token"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat("Token: ", AccessToken, ", ExpirationDate: ", ExpirationDate, ", ExpiresIn: ", ExpiresIn, ", RefreshToken: ", RefreshToken);
        }
    }
}

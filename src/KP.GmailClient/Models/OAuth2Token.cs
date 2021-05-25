using System;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>An OAuth 2.0 token.</summary>
    public class OAuth2Token
    {
        /// <summary>A string representing an authorization issued to the client.</summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <summary>A credential used to obtain access tokens.</summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Provides the client with the information required to successfully utilize the access token
        /// to make a protected resource request (along with type-specific attributes), for example: Bearer.
        /// </summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        /// <summary>The lifetime in seconds of the access token.</summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>The expiration date of the access token.</summary>
        [JsonPropertyName("expiry_date")]
        public DateTimeOffset ExpirationDate { get; set; }

        /// <summary>A string with the values of the properties from this <see cref="OAuth2Token"/>.</summary>
        public override string ToString()
        {
            return string.Concat(
                "ExpirationDate: ", ExpirationDate,
                ", ExpiresIn: ", ExpiresIn,
                ", TokenType: ", TokenType,
                ", AccessToken: ", string.IsNullOrWhiteSpace(AccessToken) ? "N" : "Y",
                ", RefreshToken: ", string.IsNullOrWhiteSpace(RefreshToken) ? "N" : "Y"
                );
        }
    }
}

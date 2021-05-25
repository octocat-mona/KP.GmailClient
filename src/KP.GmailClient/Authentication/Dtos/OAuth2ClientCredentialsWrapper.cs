using System.Text.Json.Serialization;
using KP.GmailClient.Models;

namespace KP.GmailClient.Authentication.Dtos
{
    internal class OAuth2ClientCredentialsWrapper
    {
        [JsonPropertyName("installed")]
        public OAuth2ClientCredentials Credentials { get; set; }
    }
}

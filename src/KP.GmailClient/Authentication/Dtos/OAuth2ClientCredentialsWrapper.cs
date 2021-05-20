using KP.GmailClient.Models;
using Newtonsoft.Json;

namespace KP.GmailClient.Authentication.Dtos
{
    internal class OAuth2ClientCredentialsWrapper
    {
        [JsonProperty("installed")]
        public OAuth2ClientCredentials Credentials { get; set; }
    }
}

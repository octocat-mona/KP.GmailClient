using Newtonsoft.Json;

namespace KP.GmailClient.Models
{
    public class OAuth2ClientCredentials
    {
        /// <summary>The client ID obtained from the <see href="https://console.developers.google.com/">API Console</see>.</summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>The client secret obtained from the <see href="https://console.developers.google.com/">API Console</see>.</summary>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>The token endpoint used to retreive new tokens, defaults to <see href="https://oauth2.googleapis.com/token"></see>.</summary>
        [JsonProperty("token_uri")]
        public string TokenUri { get; set; } = "https://oauth2.googleapis.com/token";

        /// <summary>The authorization endpoint used to start the authorization flow, defaults to <see href="https://accounts.google.com/o/oauth2/auth"></see>.</summary>
        [JsonProperty("auth_uri")]
        internal string AuthUri { get; set; } = "https://accounts.google.com/o/oauth2/auth";

        [JsonProperty("project_id")]
        internal string ProjectId { get; set; }

        [JsonProperty("auth_provider_x509_cert_url")]
        internal string AuthProviderX509CertUrl { get; set; }

        [JsonProperty("redirect_uris")]
        internal string[] RedirectUris { get; set; }
    }
}

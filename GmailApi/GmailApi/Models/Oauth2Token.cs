using System;
using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class Oauth2Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty]
        public DateTime ExpirationDate { get; set; }

        public override string ToString()
        {
            return string.Concat("Token: ", AccessToken, ", ExpirationDate: ", ExpirationDate, ", ExpiresIn: ", ExpiresIn, ", RefreshToken: ", RefreshToken);
        }
    }
}

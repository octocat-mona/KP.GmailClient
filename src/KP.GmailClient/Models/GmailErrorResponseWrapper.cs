using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    internal class GmailErrorResponseWrapper
    {
        [JsonPropertyName("error")]
        public GmailErrorResponse Error { get; set; }
    }
}

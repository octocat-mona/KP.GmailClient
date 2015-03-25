using Newtonsoft.Json;

namespace GmailApi.Models
{
    public enum MimeType
    {
        [JsonProperty("")]
        Unknown,

        [JsonProperty("text/plain")]
        TextPlain,

        [JsonProperty("text/html")]
        TextHtml
    }
}
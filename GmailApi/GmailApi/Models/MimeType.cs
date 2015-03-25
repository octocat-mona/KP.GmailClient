using Newtonsoft.Json;

namespace ConsoleApplication1.Models
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
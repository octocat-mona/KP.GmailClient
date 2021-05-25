using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>A Multipurpose Internet Mail Extensions (MIME) type.</summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum MimeType
    {
        /// <summary>Not a predefined MIME type.</summary>
        [EnumMember]
        Unknown,

        /// <summary>The text/plain MIME type.</summary>
        [EnumMember(Value = "text/plain")]
        TextPlain,

        /// <summary>The text/html MIME type.</summary>
        [EnumMember(Value = "text/html")]
        TextHtml,

        /// <summary>The application/javascript MIME type.</summary>
        [EnumMember(Value = "application/javascript")]
        ApplicationJavascript
    }
}
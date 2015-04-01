using System.Runtime.Serialization;

namespace GmailApi.Models
{
    public enum MimeType
    {
        [EnumMember]
        Unknown,

        [EnumMember(Value = "text/plain")]
        TextPlain,

        [EnumMember(Value = "text/html")]
        TextHtml,

        [EnumMember(Value = "application/javascript")]
        ApplicationJavascript
    }
}
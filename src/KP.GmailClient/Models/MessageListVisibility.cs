using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>The visibility of messages with this label in the message list in the Gmail web interface.</summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum MessageListVisibility
    {
        /// <summary>Show the label in the message list. (Default)</summary>
        [EnumMember(Value = "show")]
        Show,

        /// <summary>Do not show the label in the message list.</summary>
        [EnumMember(Value = "hide")]
        Hide,
    }
}
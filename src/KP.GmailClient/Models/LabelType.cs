using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>
    /// The owner type for the label.
    /// User labels are created by the user and can be modified and deleted by the user and can be applied to any message or thread.
    /// System labels are internally created and cannot be added, modified, or deleted.
    /// System labels may be able to be applied to or removed from messages and threads under some circumstances but this is not guaranteed.
    /// For example, users can apply and remove the INBOX and UNREAD labels from messages and threads, but cannot apply or remove the DRAFTS or SENT labels from messages or threads.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum LabelType
    {
        /// <summary>Custom labels created by the user or application.</summary>
        [EnumMember(Value = "user")]
        User,

        /// <summary>Labels created by Gmail.</summary>
        [EnumMember(Value = "system")]
        System
    }
}
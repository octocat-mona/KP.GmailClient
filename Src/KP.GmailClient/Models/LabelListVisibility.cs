using System.Runtime.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>
    /// The visibility of the label in the label list in the Gmail web interface.
    /// </summary>
    public enum LabelListVisibility
    {
        /// <summary>
        /// Show the label in the label list. (Default)
        /// </summary>
        [EnumMember(Value = "labelShow")]
        Show,

        /// <summary>
        /// Do not show the label in the label list.
        /// </summary>
        [EnumMember(Value = "labelHide")]
        Hide,

        /// <summary>
        /// Show the label if there are any unread messages with that label.
        /// </summary>
        [EnumMember(Value = "labelShowIfUnread")]
        ShowIfUnread
    }
}
namespace KP.GmailClient.Models
{
    /// <summary>
    /// The visibility of messages with this label in the message list in the Gmail web interface.
    /// </summary>
    public enum MessageListVisibility
    {
        /// <summary>
        /// Show the label in the message list. (Default)
        /// </summary>
        Show,

        /// <summary>
        /// Do not show the label in the message list.
        /// </summary>
        Hide,
    }
}
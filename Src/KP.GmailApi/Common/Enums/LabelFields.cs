using System;

namespace KP.GmailApi.Common.Enums
{
    [Flags]
    internal enum LabelFields
    {
        /// <summary>
        /// The immutable ID of the label.
        /// </summary>
        [StringValue("id")]
        Id = 1024,

        /// <summary>
        /// The visibility of the label in the label list in the Gmail web interface.
        /// Acceptable values are:
        /// <list type="bullet">
        /// <item><description>labelShow: Show the label in the label list. (Default)</description></item>
        /// <item><description>labelHide: Do not show the label in the label list.</description></item>
        /// /// <item><description>labelShowIfUnread: Show the label if there are any unread messages with that label.</description></item>
        /// </list>
        /// </summary>
        [StringValue("labelListVisibility")]
        LabelListVisibility = 2048,

        /// <summary>
        /// The visibility of messages with this label in the message list in the Gmail web interface.
        /// Acceptable values are:
        /// <list type="bullet">
        /// <item><description>show: Show the label in the message list. (Default)</description></item>
        /// <item><description>hide: Do not show the label in the message list.</description></item>
        /// </list>
        /// </summary>
        [StringValue("messageListVisibility")]
        MessageListVisibility = 4096,

        /// <summary>
        /// The total number of messages with the label.
        /// </summary>
        [StringValue("messagesTotal")]
        MessagesTotal = 8192,

        /// <summary>
        /// The number of unread messages with the label.
        /// </summary>
        [StringValue("messagesUnread")]
        MessagesUnread = 16384,

        /// <summary>
        /// The display name of the label.
        /// </summary>
        [StringValue("name")]
        Name = 32768,

        /// <summary>
        /// The total number of threads with the label.
        /// </summary>
        [StringValue("threadsTotal")]
        ThreadsTotal = 65536,

        /// <summary>
        /// The number of unread threads with the label.
        /// </summary>
        [StringValue("threadsUnread ")]
        ThreadsUnread = 131072,

        /// <summary>
        /// The owner type for the label.
        /// User labels are created by the user and can be modified and deleted by the user and can be applied to any message or thread.
        /// System labels are internally created and cannot be added, modified, or deleted.
        /// System labels may be able to be applied to or removed from messages and threads under some circumstances but this is not guaranteed.
        /// For example, users can apply and remove the INBOX and UNREAD labels from messages and threads, but cannot apply or remove the DRAFTS or SENT labels from messages or threads.
        /// Acceptable values are:
        /// <list type="bullet">
        /// <item><description>system: Labels created by Gmail.</description></item>
        /// <item><description>user: Custom labels created by the user or application.</description></item>
        /// </list>
        /// </summary>
        [StringValue("type ")]
        Type = 262144,

        /// <summary>
        /// All the label fields
        /// </summary>
        All = Id | LabelListVisibility | MessageListVisibility | MessagesTotal | MessagesUnread | Name | ThreadsTotal | ThreadsUnread | Type
    }
}
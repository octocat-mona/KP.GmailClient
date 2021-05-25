using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>Labels are used to categorize messages and threads within the user's mailbox.</summary>
    public class Label
    {
        /// <summary>The inbox label.</summary>
        public const string Inbox = "INBOX";

        /// <summary>The sent label.</summary>
        public const string Sent = "SENT";

        /// <summary>The trash label.</summary>
        public const string Trash = "TRASH";

        /// <summary>The important label.</summary>
        public const string Important = "IMPORTANT";

        /// <summary>The unread label.</summary>
        public const string Unread = "UNREAD";

        /// <summary>The spam label.</summary>
        public const string Spam = "SPAM";

        /// <summary>The starred label.</summary>
        public const string Starred = "STARRED";

        /// <summary>The draft label.</summary>
        public const string Draft = "DRAFT";

        /// <summary>Category labels.</summary>
        public static class Category
        {
            /// <summary>Updates category label.</summary>
            public const string Updates = "CATEGORY_UPDATES";

            /// <summary>Promotions category label.</summary>
            public const string Promotions = "CATEGORY_PROMOTIONS";

            /// <summary>Social category label.</summary>
            public const string Social = "CATEGORY_SOCIAL";

            /// <summary>Personal category label.</summary>
            public const string Personal = "CATEGORY_PERSONAL";

            /// <summary>Forums category label.</summary>
            public const string Forums = "CATEGORY_FORUMS";
        }

        /// <summary>The immutable ID of the label.</summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>The display name of the label.</summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>The visibility of messages with this label in the message list in the Gmail web interface.</summary>
        [JsonPropertyName("messageListVisibility")]
        public MessageListVisibility MessageListVisibility { get; set; }

        /// <summary>The visibility of the label in the label list in the Gmail web interface.</summary>
        [JsonPropertyName("labelListVisibility")]
        public LabelListVisibility LabelListVisibility { get; set; }

        /// <summary>
        /// The owner type for the label.
        /// User labels are created by the user and can be modified and deleted by the user and can be applied to any message or thread.
        /// System labels are internally created and cannot be added, modified, or deleted.
        /// System labels may be able to be applied to or removed from messages and threads under some circumstances but this is not guaranteed.
        /// For example, users can apply and remove the INBOX and UNREAD labels from messages and threads, but cannot apply or remove the DRAFTS or SENT labels from messages or threads.
        /// </summary>
        [JsonPropertyName("type")]
        public LabelType Type { get; set; }

        /// <summary>The total number of messages with the label.</summary>
        [JsonPropertyName("messagesTotal")]
        public int MessagesTotal { get; set; }

        /// <summary>The number of unread messages with the label.</summary>
        [JsonPropertyName("messagesUnread")]
        public int MessagesUnread { get; set; }

        /// <summary>The total number of threads with the label.</summary>
        [JsonPropertyName("threadsTotal")]
        public int ThreadsTotal { get; set; }

        /// <summary>The number of unread threads with the label.</summary>
        [JsonPropertyName("threadsUnread")]
        public int ThreadsUnread { get; set; }

        /// <summary>If the <see cref="Label"/> is a system label.</summary>
        [JsonIgnore]
        public bool IsSystemLabel => Type == LabelType.System;

        /// <summary>If the <see cref="Label"/> is a user defined label.</summary>
        [JsonIgnore]
        public bool IsUserLabel => Type == LabelType.User;

        /// <summary>A string with the values of the properties from this <see cref="Label"/></summary>
        public override string ToString()
        {
            return string.Concat("ID: ", Id, ", Name: ", Name, ", Type: ", Type, ", Total messages: ", MessagesTotal, ", Unread messages: ", MessagesUnread);
        }
    }
}
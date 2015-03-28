using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class Label
    {
        public const string Inbox = "INBOX";
        public const string Sent = "SENT";
        public const string Trash = "TRASH";
        public const string Important = "IMPORTANT";
        public const string Unread = "UNREAD";
        public const string Spam = "SPAM";
        public const string Starred = "STARRED";
        public const string Draft = "DRAFT";

        public static class Category
        {
            public const string Updates = "CATEGORY_UPDATES";
            public const string Promotions = "CATEGORY_PROMOTIONS";
            public const string Social = "CATEGORY_SOCIAL";
            public const string Personal = "CATEGORY_PERSONAL";
            public const string Forums = "CATEGORY_FORUMS";
        }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("messageListVisibility")]
        public string MessageListVisibility { get; set; }

        [JsonProperty("labelListVisibility")]
        public string LabelListVisibility { get; set; }

        [JsonProperty("type")]
        public LabelType Type { get; set; }

        [JsonProperty("messagesTotal")]
        public int MessagesTotal { get; set; }

        [JsonProperty("messagesUnread")]
        public int MessagesUnread { get; set; }

        [JsonProperty("threadsTotal")]
        public int ThreadsTotal { get; set; }

        [JsonProperty("threadsUnread")]
        public int ThreadsUnread { get; set; }

        public override string ToString()
        {
            return string.Concat("ID: ", Id, ", Name: ", Name, ", Type: ", Type, ", Total messages: ", MessagesTotal, ", Unread messages: ", MessagesUnread);
        }
    }
}
namespace GmailApi.Models
{
    public class Label
    {
        public const string Inbox = "INBOX";
        public const string Sent = "SENT";
        public const string Trash = "TRASH";
        public const string Important = "IMPORTANT";
        public const string Unread = "UNREAD";

        public static class Category
        {
            public const string Updates = "CATEGORY_UPDATES";
            public const string Promotions = "CATEGORY_PROMOTIONS";
            public const string Social = "CATEGORY_SOCIAL";
            public const string Personal = "CATEGORY_PERSONAL";
            public const string Forums = "CATEGORY_FORUMS";
        }
    }
}
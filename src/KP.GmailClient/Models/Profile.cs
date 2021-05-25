using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>Gets the current user's Gmail profile.</summary>
    public class Profile
    {
        /// <summary>The user's email address.</summary>
        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }

        /// <summary>The total number of messages in the mailbox.</summary>
        [JsonPropertyName("messagesTotal")]
        public int MessagesTotal { get; set; }

        /// <summary>The total number of threads in the mailbox.</summary>
        [JsonPropertyName("threadsTotal")]
        public int ThreadsTotal { get; set; }

        /// <summary>The ID of the mailbox's current history record.</summary>
        [JsonPropertyName("historyId")]
        public ulong HistoryId { get; set; }

        /// <summary>A string with the values of the properties from this <see cref="Profile"/></summary>
        public override string ToString()
        {
            return string.Concat("EmailAddress: ", EmailAddress, ", Total messages: ", MessagesTotal, ", Total Threads: ", ThreadsTotal, ", History ID: ", HistoryId);
        }
    }
}
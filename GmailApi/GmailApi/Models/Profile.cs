using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class Profile
    {
        /// <summary>
        /// The user's email address.
        /// </summary>
        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// The total number of messages in the mailbox.
        /// </summary>
        [JsonProperty("messagesTotal")]
        public int MessagesTotal { get; set; }

        /// <summary>
        /// The total number of threads in the mailbox.
        /// </summary>
        [JsonProperty("threadsTotal")]
        public int ThreadsTotal { get; set; }

        /// <summary>
        /// The ID of the mailbox's current history record.
        /// </summary>
        [JsonProperty("historyId")]
        public ulong HistoryId { get; set; }

        /// <summary>
        /// A string with the values of the properties from this <see cref="Profile"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat("EmailAddress: ", EmailAddress, ", Total messages: ", MessagesTotal, ", Total Threads: ", ThreadsTotal, ", History ID: ", HistoryId);
        }
    }
}
using Newtonsoft.Json;

namespace ConsoleApplication1
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

        public override string ToString()
        {
            return string.Concat("EmailAddress: ", EmailAddress, ", Total messages: ", MessagesTotal, ", Total Threads: ", ThreadsTotal, ", History ID: ", HistoryId);
        }
    }
}
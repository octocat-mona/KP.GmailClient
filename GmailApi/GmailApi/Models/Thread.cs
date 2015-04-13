using System.Collections.Generic;
using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class Thread
    {
        public Thread()
        {
            Id = string.Empty;
            Snippet = string.Empty;
            Messages = new List<Message>(0);
        }

        /// <summary>
        /// The unique ID of the thread.
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// A short part of the message text.
        /// </summary>
        [JsonProperty("snippet")]
        public string Snippet { get; set; }

        /// <summary>
        /// The ID of the last history record that modified this thread.
        /// </summary>
        [JsonProperty("historyId")]
        public ulong HistoryId { get; set; }

        /// <summary>
        /// The list of messages in the thread.
        /// </summary>
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }

        public override string ToString()
        {
            return string.Concat("ID: ", Id, ", Snippet: ", Snippet, ", History ID: ", HistoryId, ", # Messages: ", Messages.Count);
        }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class MessageList
    {
        public MessageList()
        {
            Messages = new List<Message>(0);
            NextPageToken = string.Empty;
        }

        /// <summary>
        /// List with messages, with only the ID and/or ThreadID set.
        /// </summary>
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }

        /// <summary>
        /// Token to retrieve the next page of results in the list.
        /// </summary>
        [JsonProperty("nextPageToken")]
        public string NextPageToken { get; set; }

        /// <summary>
        /// Estimated total number of results.
        /// </summary>
        [JsonProperty("resultSizeEstimate")]
        public uint ResultSizeEstimate { get; set; }

        public override string ToString()
        {
            return string.Concat("# Messages: ",Messages.Count,", NextPageToken: ",NextPageToken,", ");
        }
    }
}

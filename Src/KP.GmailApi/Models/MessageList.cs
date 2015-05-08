using System.Collections.Generic;
using Newtonsoft.Json;

namespace KP.GmailApi.Models
{
    /// <summary>
    /// Contains a list of messages.
    /// </summary>
    public class MessageList
    {
        /// <summary>
        /// Contains a list of messages.
        /// </summary>
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

        /// <summary>
        /// A string with the values of the properties from this <see cref="MessageList"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat("# Messages: ", Messages.Count, ", NextPageToken: ", NextPageToken, ", SizeEstimate: ", ResultSizeEstimate);
        }
    }
}

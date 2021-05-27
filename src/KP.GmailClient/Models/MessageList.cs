using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>Contains a list of messages.</summary>
    public class MessageList
    {
        /// <summary>List with messages, with only the ID and/or ThreadID set.</summary>
        [JsonPropertyName("messages")]
        public List<Message> Messages { get; set; } = new();

        /// <summary>Token to retrieve the next page of results in the list.</summary>
        [JsonPropertyName("nextPageToken")]
        public string NextPageToken { get; set; }

        /// <summary>Estimated total number of results.</summary>
        [JsonPropertyName("resultSizeEstimate")]
        public uint ResultSizeEstimate { get; set; }

        /// <summary>A string with the values of the properties from this <see cref="MessageList"/></summary>
        public override string ToString()
        {
            return string.Concat("# Messages: ", Messages.Count, ", NextPageToken: ", NextPageToken, ", SizeEstimate: ", ResultSizeEstimate);
        }
    }
}

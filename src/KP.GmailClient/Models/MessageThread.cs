using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>A collection of messages representing a conversation.</summary>
    public class MessageThread
    {
        /// <summary>The unique ID of the thread.</summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>A short part of the message text.</summary>
        [JsonPropertyName("snippet")]
        public string Snippet { get; set; }

        /// <summary>The ID of the last history record that modified this thread.</summary>
        [JsonPropertyName("historyId")]
        public string HistoryId { get; set; }

        /// <summary>The list of messages in the thread.</summary>
        [JsonPropertyName("messages")]
        public List<Message> Messages { get; set; } = new();

        /// <summary>A string with the values of the properties from this <see cref="MessageThread"/></summary>
        public override string ToString()
        {
            return string.Concat("ID: ", Id, ", Snippet: ", Snippet, ", History ID: ", HistoryId, ", # Messages: ", Messages.Count);
        }
    }
}

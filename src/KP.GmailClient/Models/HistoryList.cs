using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>Contains the history of added and deleted messages and labels.</summary>
    public class HistoryList
    {
        /// <summary>
        /// List of history records.
        /// Any messages contained in the response will typically only have id and threadId fields populated.
        /// </summary>
        [JsonPropertyName("history")]
        public List<History> Histories { get; set; } = new List<History>();

        /// <summary>The mailbox sequence ID.</summary>
        [JsonPropertyName("historyId")]
        public ulong HistoryId { get; set; }

        /// <summary>Token to retrieve the next page of results in the list.</summary>
        [JsonPropertyName("nextPageToken")]
        public string NextPageToken { get; set; }

        /// <summary>A string with the values of the properties from this <see cref="HistoryList"/>.</summary>
        public override string ToString()
        {
            return string.Concat("# Messages: ", Histories.Count, ", NextPageToken: ", NextPageToken, ", History ID: ", HistoryId);
        }
    }
}

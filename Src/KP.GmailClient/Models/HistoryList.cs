using System.Collections.Generic;
using Newtonsoft.Json;

namespace KP.GmailClient.Models
{
    /// <summary>
    /// Contains the history of added and deleted messages and labels.
    /// </summary>
    public class HistoryList
    {
        /// <summary>
        /// Contains the history of added and deleted messages and labels.
        /// </summary>
        public HistoryList()
        {
            Histories = new List<History>(0);
            NextPageToken = string.Empty;
        }

        /// <summary>
        /// List of history records.
        /// Any messages contained in the response will typically only have id and threadId fields populated.
        /// </summary>
        [JsonProperty("history")]
        public List<History> Histories { get; set; }

        /// <summary>
        /// The mailbox sequence ID.
        /// </summary>
        [JsonProperty("historyId")]
        public ulong HistoryId { get; set; }

        /// <summary>
        /// Token to retrieve the next page of results in the list.
        /// </summary>
        [JsonProperty("nextPageToken")]
        public string NextPageToken { get; set; }

        /// <summary>
        /// A string with the values of the properties from this <see cref="HistoryList"/>.
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat("# Messages: ", Histories.Count, ", NextPageToken: ", NextPageToken, ", History ID: ", HistoryId);
        }
    }
}

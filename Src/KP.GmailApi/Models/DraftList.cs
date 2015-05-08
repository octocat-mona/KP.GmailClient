using System.Collections.Generic;
using Newtonsoft.Json;

namespace KP.GmailApi.Models
{
    /// <summary>
    /// Lists the drafts in the user's mailbox.
    /// </summary>
    public class DraftList
    {
        /// <summary>
        /// Lists the drafts in the user's mailbox.
        /// </summary>
        public DraftList()
        {
            Drafts = new List<Draft>(0);
            NextPageToken = string.Empty;
        }

        /// <summary>
        /// List of drafts.
        /// </summary>
        [JsonProperty("drafts")]
        public List<Draft> Drafts { get; set; }

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
        /// A string with the values of the properties from this <see cref="DraftList"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat("# Drafts: ", Drafts.Count, ", NextPageToken: ", NextPageToken, ", SizeEstimate: ", ResultSizeEstimate);
        }
    }
}

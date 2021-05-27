using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>Contains a list of drafts.</summary>
    public class DraftList
    {
        /// <summary>List of drafts.</summary>
        [JsonPropertyName("drafts")]
        public List<Draft> Drafts { get; set; } = new();

        /// <summary>Token to retrieve the next page of results in the list.</summary>
        [JsonPropertyName("nextPageToken")]
        public string NextPageToken { get; set; }

        /// <summary>Estimated total number of results.</summary>
        [JsonPropertyName("resultSizeEstimate")]
        public uint ResultSizeEstimate { get; set; }

        /// <summary>A string with the values of the properties from this <see cref="DraftList"/></summary>
        public override string ToString()
        {
            return string.Concat("# Drafts: ", Drafts.Count, ", NextPageToken: ", NextPageToken, ", SizeEstimate: ", ResultSizeEstimate);
        }
    }
}

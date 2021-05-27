using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>Contains a list of threads.</summary>
    public class ThreadList
    {
        /// <summary>List with threads.</summary>
        [JsonPropertyName("threads")]
        public List<MessageThread> Threads { get; set; } = new(0);

        /// <summary>Token to retrieve the next page of results in the list.</summary>
        [JsonPropertyName("nextPageToken")]
        public string NextPageToken { get; set; }

        /// <summary>Estimated total number of results.</summary>
        [JsonPropertyName("resultSizeEstimate")]
        public uint ResultSizeEstimate { get; set; }

        /// <summary>A string with the values of the properties from this <see cref="ThreadList"/></summary>
        public override string ToString()
        {
            return string.Concat("# Messages: ", Threads.Count, ", NextPageToken: ", NextPageToken, ", SizeEstimate: ", ResultSizeEstimate);
        }
    }
}

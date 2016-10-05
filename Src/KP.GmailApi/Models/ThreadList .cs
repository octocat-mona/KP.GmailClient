using System.Collections.Generic;
using Newtonsoft.Json;

namespace KP.GmailApi.Models
{
    /// <summary>
    /// Contains a list of threads.
    /// </summary>
    public class ThreadList
    {
        /// <summary>
        /// Contains a list of threads.
        /// </summary>
        public ThreadList()
        {
            Threads = new List<MessageThread>(0);
            NextPageToken = string.Empty;
        }

        /// <summary>
        /// List with threads.
        /// </summary>
        [JsonProperty("threads")]
        public List<MessageThread> Threads { get; set; }

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
        /// A string with the values of the properties from this <see cref="ThreadList"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat("# Messages: ", Threads.Count, ", NextPageToken: ", NextPageToken, ", SizeEstimate: ", ResultSizeEstimate);
        }
    }
}

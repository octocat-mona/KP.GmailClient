using System.Collections.Generic;
using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class ThreadList
    {
        public ThreadList()
        {
            Threads = new List<Thread>(0);
            NextPageToken = string.Empty;
        }

        /// <summary>
        /// List with threads.
        /// </summary>
        [JsonProperty("threads")]
        public List<Thread> Threads { get; set; }

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
            return string.Concat("# Messages: ", Threads.Count, ", NextPageToken: ", NextPageToken, ", SizeEstimate: ", ResultSizeEstimate);
        }
    }
}

using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace KP.GmailClient.Models
{
    /// <summary>An error returned by Gmail.</summary>
    public class GmailErrorResponse
    {
        /// <summary>The <see cref="HttpStatusCode"/> value.</summary>
        [JsonProperty("code", Required = Required.Always)]
        public int Code { get; set; }

        /// <summary>The global message of the error(s).</summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>A list with <see cref="GmailError"/>.</summary>
        [JsonProperty("errors")]
        public List<GmailError> Errors { get; set; } = new List<GmailError>();

        /// <summary>A string with the values of the properties from this <see cref="GmailErrorResponse"/>.</summary>
        public override string ToString()
        {
            return string.Concat(Code, ": ", Message, ". ", Errors.Count, " errors.");
        }
    }
}
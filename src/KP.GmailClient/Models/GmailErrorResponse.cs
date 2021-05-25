using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace KP.GmailClient.Models
{
    /// <summary>An error returned by Gmail.</summary>
    public class GmailErrorResponse
    {
        /// <summary>The <see cref="HttpStatusCode"/> value.</summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>The global message of the error(s).</summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>A list with <see cref="GmailError"/>.</summary>
        [JsonPropertyName("errors")]
        public List<GmailError> Errors { get; set; } = new List<GmailError>();

        /// <summary>A string with the values of the properties from this <see cref="GmailErrorResponse"/>.</summary>
        public override string ToString()
        {
            return string.Concat(Code, ": ", Message, ". ", Errors.Count, " errors.");
        }
    }
}
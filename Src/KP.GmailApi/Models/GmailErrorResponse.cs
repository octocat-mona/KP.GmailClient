using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace KP.GmailApi.Models
{
    /// <summary>
    /// An error returned by Gmail.
    /// </summary>
    public class GmailErrorResponse
    {
        /// <summary>
        /// An error returned by Gmail.
        /// </summary>
        public GmailErrorResponse()
        {
            Message = string.Empty;
            Errors = new List<GmailError>(0);
        }

        /// <summary>
        /// The <see cref="HttpStatusCode"/> value.
        /// </summary>
        [JsonProperty("code", Required = Required.Always)]
        public int Code { get; set; }

        /// <summary>
        /// The global message of the error(s).
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// A list with <see cref="GmailError"/>
        /// </summary>
        [JsonProperty("errors")]
        public List<GmailError> Errors { get; set; }

        /// <summary>
        /// A string with the values of the properties from this <see cref="GmailErrorResponse"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat(Code, ": ", Message, ". ", Errors.Count, " errors.");
        }
    }
}
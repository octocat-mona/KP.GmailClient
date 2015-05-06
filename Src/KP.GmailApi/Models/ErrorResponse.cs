using System.Collections.Generic;
using Newtonsoft.Json;

namespace KP.GmailApi.Models
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Message = string.Empty;
            Errors = new List<Error>(0);
        }

        [JsonProperty("code", Required = Required.Always)]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }

        /// <summary>
        /// A string with the values of the properties from this <see cref="ErrorResponse"/>
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            return string.Concat(Code, ": ", Message, ". ", Errors.Count, " errors.");
        }
    }
}
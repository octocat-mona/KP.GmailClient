using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    public class Response//TODO: convert to exception? -> check Json convert
    {
        //TODO: json attr?
        public List<Error> Errors { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
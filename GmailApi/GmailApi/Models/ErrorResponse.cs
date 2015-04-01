using System.Collections.Generic;
using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class ErrorResponse//TODO: convert to exception? -> check Json convert
    {
        //TODO: json attr?
        public List<Error> Errors { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}

/*
Bad Request:{

 "error": {

  "errors": [

   {

    "domain": "global",

    "reason": "parseError",

    "message": "Parse Error"

   }

  ],

  "code": 400,

  "message": "Parse Error"

 }

}
*/
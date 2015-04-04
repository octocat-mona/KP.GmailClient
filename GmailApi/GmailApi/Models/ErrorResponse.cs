using System.Collections.Generic;
using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class ErrorResponse
    {
        [JsonProperty("errors")]
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

{
   "error":{
      "errors":[
         {
            "domain":"global",
            "reason":"invalidParameter",
            "message":"Invalid field selection MessageId",
            "locationType":"parameter",
            "location":"fields"
         }
      ],
      "code":400,
      "message":"Invalid field selection MessageId"
   }
}
*/
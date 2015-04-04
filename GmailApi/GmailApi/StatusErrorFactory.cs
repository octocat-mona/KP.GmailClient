using GmailApi.Models;
using Newtonsoft.Json;

namespace GmailApi
{
    internal class StatusErrorFactory
    {
        public static GmailException Create(string content)
        {
            // parse content
            // get int statuscode
            // get errorresponse

            var err = JsonConvert.DeserializeObject<ErrorResponse>(content);

            return new GmailException(err);

            //throw new Exception(string.Concat(resMessage.ReasonPhrase, ":", content));
        }
    }
}
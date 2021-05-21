using System.Net;
using KP.GmailClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KP.GmailClient.Common
{
    internal class ErrorResponseParser
    {
        /// <summary>Parses the <paramref name="content"/> to an Gmail API error response.</summary>
        /// <param name="statusCode"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        internal static GmailApiException Parse(HttpStatusCode statusCode, string content)
        {
            try
            {
                JObject jObject = JObject.Parse(content);
                JToken errorContent = jObject.SelectToken("error", true);
                GmailErrorResponse errorResponse = errorContent.ToObject<GmailErrorResponse>();
                return new GmailApiException(errorResponse);
            }
            catch (JsonException)
            {
                return new GmailApiException(statusCode, content);
            }
        }
    }
}
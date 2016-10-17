using System.Net;
using KP.GmailApi.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace KP.GmailApi.Common
{
    internal class ErrorResponseParser
    {
        /// <summary>
        /// Parses the <paramref name="content"/> to an Gmail API error response.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static GmailException Parse(HttpStatusCode statusCode, string content)
        {
            JObject jObject = JObject.Parse(content);
            JToken errorContent = jObject.SelectToken("error", false);

            // Return just the StatusCode and content in case the response is not an Gmail API error
            if (errorContent == null || !jObject.IsValid(JsonSchema))
            {
                return new GmailException(statusCode, content);
            }

            GmailErrorResponse errorResponse = errorContent.ToObject<GmailErrorResponse>();
            return new GmailException(errorResponse);
        }

        private static readonly JsonSchema JsonSchema = JsonSchema.Parse(JsonSchemaString);
        private const string JsonSchemaString = @"
{
    'type': 'object',
    'properties':
    {
        'error':
        {
            'type': 'object',
            'properties':
            {
                'code': { 'type': 'integer', 'required': 'true' },
                'message': { 'type': 'string', 'required': 'true' },
                'errors':
                {
                    'type': 'array',
                    'properties':
                    {
                        'domain': { 'type': 'string' },
                        'reason': { 'type': 'string' },
                        'message': { 'type': 'string' },
                        'locationType': { 'type': 'string' },
                        'location': { 'type': 'string' }
                    }
                }                
            }
        }
    }
}
";
    }
}
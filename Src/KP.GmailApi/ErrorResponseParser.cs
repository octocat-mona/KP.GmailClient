using System.Net;
using KP.GmailApi.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace KP.GmailApi
{
    internal class ErrorResponseParser
    {
        public static GmailException Parse(HttpStatusCode statusCode, string content)
        {
            JObject jObject = JObject.Parse(content);
            JToken errorContent = jObject.SelectToken("error", false);

            // Return just the StatusCode and content in case the response is not an Gmail API error
            if (!jObject.IsValid(JsonSchema) || errorContent == null)
                return new GmailException(statusCode, content);

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
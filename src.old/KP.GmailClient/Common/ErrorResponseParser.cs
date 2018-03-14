using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KP.GmailClient.Models;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using NJsonSchema.Validation;

namespace KP.GmailClient.Common
{
    internal class ErrorResponseParser
    {
        private static readonly Lazy<Task<JsonSchema4>> GetJsonSchema = new Lazy<Task<JsonSchema4>>(()
            => JsonSchema4.FromJsonAsync(JsonSchemaString));

        /// <summary>
        /// Parses the <paramref name="content"/> to an Gmail API error response.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task<GmailException> ParseAsync(HttpStatusCode statusCode, string content)
        {
            var schema = await GetJsonSchema.Value;
            // Return just the StatusCode and content in case the response is not an Gmail API error
            var errors = new JsonSchemaValidator().Validate(content, schema);
            if (errors.Any())
            {
                return new GmailException(statusCode, content);
            }

            JObject jObject = JObject.Parse(content);
            JToken errorContent = jObject.SelectToken("error", false);
            GmailErrorResponse errorResponse = errorContent.ToObject<GmailErrorResponse>();
            return new GmailException(errorResponse);
        }

        private const string JsonSchemaString = @"
{
  '$schema': 'http://json-schema.org/draft-04/schema#',
  'type': 'object',
  'required': [
    'code',
	'message',
  ],
  'properties': {
    'error': {
      '$ref': '#/definitions/Anonymous1'
    }
  },
  'definitions': {
    'Anonymous1': {
      'type': 'object',
      'properties': {
        'errors': {
          'type': 'array',
          'items': {
            '$ref': '#/definitions/Anonymous2'
          }
        },
        'code': {
          'type': 'integer'
        },
        'message': {
          'type': 'string'
        }
      }
    },
    'Anonymous2': {
      'type': 'object',
      'properties': {
        'domain': {
          'type': 'string'
        },
        'reason': {
          'type': 'string'
        },
        'message': {
          'type': 'string'
        },
        'locationType': {
          'type': 'string'
        },
        'location': {
          'type': 'string'
        }
      }
    }
  }
}
";
    }
}
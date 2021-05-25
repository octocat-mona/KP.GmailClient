using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using KP.GmailClient.Models;

namespace KP.GmailClient.Common
{
    internal class ErrorResponseParser
    {
        /// <summary>Parses the <paramref name="stream"/> to an Gmail API error response.</summary>
        /// <param name="statusCode">The original HTTP status code included in the error when the content could not be parsed.</param>
        /// <param name="stream">Stream to read the content from, does not close the stream.</param>
        internal static async Task<GmailApiException> ParseAsync(HttpStatusCode statusCode, Stream stream)
        {
            async Task<GmailApiException> CreateFromRawContent()
            {
                using var reader = new StreamReader(stream, Encoding.UTF8);
                stream.Seek(0, SeekOrigin.Begin);
                string content = await reader.ReadToEndAsync();
                return new GmailApiException(statusCode, content);
            }

            try
            {
                var error = (await JsonSerializer.DeserializeAsync<GmailErrorResponseWrapper>(stream))?.Error;
                return error == null
                    ? await CreateFromRawContent()
                    : new GmailApiException(error);
            }
            catch (JsonException)
            {
                return await CreateFromRawContent();
            }
        }
    }
}
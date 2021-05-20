using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace KP.GmailClient.Common
{
    /// <summary>Handles requests to the Gmail service and parses the response.</summary>
    internal class GmailProxy : IDisposable
    {
        /// <summary>The URL to send requests to the Gmail API service.</summary>
        public const string ApiBaseUrl = "https://www.googleapis.com/gmail/v1/users/";
        private static readonly HttpMethod HttpGet = new HttpMethod("GET");
        private static readonly HttpMethod HttpPost = new HttpMethod("POST");
        private static readonly HttpMethod HttpPut = new HttpMethod("PUT");
        private static readonly HttpMethod HttpPatch = new HttpMethod("PATCH");
        private static readonly HttpMethod HttpDelete = new HttpMethod("DELETE");

        private readonly HttpClient _client;

        /// <summary>Takes care of all I/O to Gmail.</summary>
        /// <param name="handler">An optional handler to handle authentication or caching for example</param>
        internal GmailProxy(DelegatingHandler handler)
        {
            var compressionHandler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            HttpMessageHandler mainHandler;
            if (handler == null)
            {
                mainHandler = compressionHandler;
            }
            else
            {
                handler.InnerHandler = compressionHandler;
                mainHandler = handler;
            }

            _client = new HttpClient(mainHandler) { BaseAddress = new Uri(string.Concat(ApiBaseUrl, "me/")) };
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

            // Set default (de)serializing for enums
            var stringEnumConverter = new StringEnumConverter { CamelCaseText = true };
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings { Converters = { stringEnumConverter } };
        }

        internal async Task<T> Get<T>(string queryString)
        {
            return await GetResponseAsync<T>(HttpGet, queryString);
        }

        internal async Task<T> Get<T>(string queryString, ParseOptions options)
        {
            var jObject = await GetResponseAsync<JObject>(HttpGet, queryString);
            return jObject.SelectToken(options.Path, true).ToObject<T>();
        }

        internal async Task<T> Post<T>(string queryString, object content = null)
        {
            return await GetResponseAsync<T>(HttpPost, queryString, content);
        }

        internal async Task<T> Put<T>(string queryString, object content = null)
        {
            return await GetResponseAsync<T>(HttpPut, queryString, content);
        }

        internal async Task<T> Patch<T>(string queryString, object content = null)
        {
            return await GetResponseAsync<T>(HttpPatch, queryString, content);
        }

        internal async Task Delete(string queryString)
        {
            await GetResponseAsync<object>(HttpDelete, queryString);
        }

        private async Task<T> GetResponseAsync<T>(HttpMethod httpMethod, string queryString, object content = null)
        {
            HttpContent httpContent = content == null
                ? null
                : new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(httpMethod, queryString)
            {
                Content = httpContent
            };

            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                string contentString = await response.Content.ReadAsStringAsync();
                GmailApiException ex = await ErrorResponseParser.ParseAsync(response.StatusCode, contentString);
                throw ex;
            }

            using (Stream stream = await response.Content.ReadAsStreamAsync())
            {
                return Serializer.Deserialize<T>(stream);
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}

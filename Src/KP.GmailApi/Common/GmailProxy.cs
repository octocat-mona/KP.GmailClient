using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace KP.GmailApi.Common
{
    /// <summary>
    /// Handles requests to the Gmail service and parses the response.
    /// </summary>
    internal class GmailProxy
    {
        /// <summary>
        /// The URL to send requests to the Gmail API service
        /// </summary>
        public const string ApiBaseUrl = "https://www.googleapis.com/gmail/v1/users/";
        private const string HttpGet = "GET";
        private const string HttpPost = "POST";
        private const string HttpPut = "PUT";
        private const string HttpPatch = "PATCH";
        private const string HttpDelete = "DELETE";

        private readonly HttpClient _client;
        private readonly JsonSerializer _jsonSerializer;

        /// <summary>
        /// Takes care of all I/O to Gmail.
        /// </summary>
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
            _jsonSerializer = new JsonSerializer { Converters = { stringEnumConverter } };
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

        private async Task<T> GetResponseAsync<T>(string httpMethod, string queryString, object content = null)
        {
            HttpContent httpContent = content == null
                ? null
                : new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(new HttpMethod(httpMethod), queryString)
            {
                Content = httpContent
            };

            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                string contentString = await response.Content.ReadAsStringAsync();
                GmailException ex = ErrorResponseParser.Parse(response.StatusCode, contentString);
                throw ex;
            }

            using (Stream stream = await response.Content.ReadAsStreamAsync())
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(streamReader))
            {
                return _jsonSerializer.Deserialize<T>(jsonReader);
            }
        }
    }
}

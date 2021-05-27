using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using KP.GmailClient.Extensions;

namespace KP.GmailClient.Common
{
    /// <summary>Handles requests to the Gmail service and parses the response.</summary>
    internal class GmailProxy : IDisposable
    {
        /// <summary>The URL to send requests to the Gmail API service.</summary>
        public const string ApiBaseUrl = "https://www.googleapis.com/gmail/v1/users/";
        private static readonly HttpMethod HttpGet = new("GET");
        private static readonly HttpMethod HttpPost = new("POST");
        private static readonly HttpMethod HttpPut = new("PUT");
        private static readonly HttpMethod HttpPatch = new("PATCH");

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
        }

        internal async Task<T> Get<T>(string queryString)
        {
            return await GetResponseAsync<T>(HttpGet, queryString);
        }

        internal async Task<T> Get<T>(string queryString, ParseOptions options)
        {
            var element = await GetResponseAsync<JsonElement>(HttpGet, queryString);
            return element.GetProperty(options.Path).ToObject<T>();
        }

        internal async Task<T> Post<T>(string queryString, object content = null)
        {
            return await GetResponseAsync<T>(HttpPost, queryString, content);
        }

        internal async Task<T> Put<T>(string queryString, object content = null)
        {
            return await GetResponseAsync<T>(HttpPut, queryString, content);
        }

        internal async Task<T> Patch<T>(string queryString, object content)
        {
            return await GetResponseAsync<T>(HttpPatch, queryString, content);
        }

        internal async Task Delete(string queryString)
        {
            var response = await _client.DeleteAsync(queryString);
            await EnsureSuccessResponseAsync(response);
        }

        private async Task<T> GetResponseAsync<T>(HttpMethod httpMethod, string queryString, object content = null)
        {
            var request = new HttpRequestMessage(httpMethod, queryString)
            {
                Content = content == null ? null : JsonContent.Create(content)
            };

            HttpResponseMessage response = await _client.SendAsync(request);
            await EnsureSuccessResponseAsync(response);

            using Stream stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }

        private static async Task EnsureSuccessResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            using Stream stream = await response.Content.ReadAsStreamAsync();
            GmailApiException ex = await ErrorResponseParser.ParseAsync(response.StatusCode, stream);
            throw ex;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}

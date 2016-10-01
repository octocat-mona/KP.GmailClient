using System;
using System.Net;
using System.Net.Http;
using System.Text;
using KP.GmailApi.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace KP.GmailApi
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
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter { CamelCaseText = true } }
            };
        }

        internal T Get<T>(string queryString)
        {
            string response = GetResponse(HttpGet, queryString);

            return JsonConvert.DeserializeObject<T>(response);
        }

        internal T Get<T>(string queryString, ParseOptions options)
        {
            string response = GetResponse(HttpGet, queryString);

            var jo = JObject.Parse(response);
            return jo.SelectToken(options.Path, true).ToObject<T>();
        }

        internal T Post<T>(string queryString, object content = null)
        {
            string response = GetResponse(HttpPost, queryString, content);

            return JsonConvert.DeserializeObject<T>(response);
        }

        internal T Put<T>(string queryString, object content = null)
        {
            string response = GetResponse(HttpPut, queryString, content);

            return JsonConvert.DeserializeObject<T>(response);
        }

        internal T Patch<T>(string queryString, object content = null)
        {
            string response = GetResponse(HttpPatch, queryString, content);

            return JsonConvert.DeserializeObject<T>(response);
        }

        internal void Delete(string queryString)
        {
            GetResponse(HttpDelete, queryString);
        }

        private string GetResponse(string httpMethod, string queryString, object content = null)
        {
            HttpContent httpContent = content == null
                ? null
                : new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(new HttpMethod(httpMethod), queryString)
            {
                Content = httpContent
            };

            HttpResponseMessage response = _client.SendAsync(request).Result;

            string contentString = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
                return contentString;

            GmailException ex = ErrorResponseParser.Parse(response.StatusCode, contentString);
            throw ex;
        }
    }
}

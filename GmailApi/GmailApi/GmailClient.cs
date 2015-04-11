using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace GmailApi
{
    /// <summary>
    /// Handles requests to the Gmail service and parses the response.
    /// </summary>
    public class GmailClient //TODO: interface
    {
        public const string ApiBaseUrl = "https://www.googleapis.com/gmail/v1/users/";
        private readonly TokenManager _tokenManager;
        private readonly Uri _baseAddress;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">The user's email address. The special value 'me' can be used to indicate the authenticated user.</param>
        /// <param name="tokenManager"></param>
        public GmailClient(string userId, TokenManager tokenManager)
        {
            userId = HttpUtility.UrlEncode(userId);
            _baseAddress = new Uri(string.Concat(ApiBaseUrl, userId, "/"));
            _tokenManager = tokenManager;

            // Set default (de)serializing for enums
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter { CamelCaseText = true } }
            };
        }

        public T Get<T>(string queryString)
        {
            var res = GetClient()
                .GetAsync(queryString);

            return ParseResponse<T>(res);
        }

        public T Get<T>(string queryString, ParseOptions options)
        {
            var res = GetClient()
                .GetAsync(queryString);

            return ParseResponse<T>(res, options);
        }

        public T Post<T>(string queryString, object content = null)
        {
            HttpContent httpContent = GetHttpContent<T>(content);

            var res = GetClient()
                .PostAsync(queryString, httpContent);

            return ParseResponse<T>(res);
        }

        public T Put<T>(string queryString, object content = null)
        {
            HttpContent httpContent = GetHttpContent<T>(content);

            var res = GetClient()
                .PutAsync(queryString, httpContent);

            return ParseResponse<T>(res);
        }

        public T Delete<T>(string queryString)
        {
            var res = GetClient()
                .DeleteAsync(queryString);

            return ParseResponse<T>(res);
        }

        private HttpClient GetClient()
        {
            // For example: https://www.googleapis.com/gmail/v1/users/{userId}/
            var client = new HttpClient { BaseAddress = _baseAddress };

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _tokenManager.GetToken());
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        private static HttpContent GetHttpContent<T>(object content)
        {
            var httpContent = content == null
                ? null
                : new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            return httpContent;
        }

        private static T ParseResponse<T>(Task<HttpResponseMessage> res)
        {
            string content = GetResponse(res);
            return JsonConvert.DeserializeObject<T>(content);
        }

        private static T ParseResponse<T>(Task<HttpResponseMessage> res, ParseOptions parseOptions)
        {
            string content = GetResponse(res);

            var jo = JObject.Parse(content);
            return jo.SelectToken(parseOptions.Path, true).ToObject<T>();
        }

        private static string GetResponse(Task<HttpResponseMessage> res)
        {
            var response = res.Result;
            string content = response.Content.ReadAsStringAsync().Result;

            if (!response.IsSuccessStatusCode)
            {
                Exception ex = ErrorResponseParser.Parse(response.StatusCode, content);
                throw ex;
            }

            return content;
        }
    }
}

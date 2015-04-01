using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace GmailApi
{
    public class GmailClient //TODO: interface
    {
        private readonly TokenManager _tokenManager;
        private readonly Uri _baseAddress;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl">Base URL of the service, for example: 'https://www.googleapis.com/gmail/v1/users/'</param>
        /// <param name="userId">The user's email address. The special value 'me' can be used to indicate the authenticated user.</param>
        /// <param name="tokenManager"></param>
        public GmailClient(string baseUrl, string userId, TokenManager tokenManager)
        {
            _baseAddress = new Uri(string.Concat(baseUrl.PadRight(1, '/'), userId, "/"));
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
            var httpContent = content == null
                ? null
                : new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var res = GetClient()
                .PostAsync(queryString, httpContent);

            return ParseResponse<T>(res);
        }

        public T Put<T>(string queryString)
        {
            throw new NotImplementedException();
            //return default(T);
        }

        public T Delete<T>(string queryString)
        {
            throw new NotImplementedException();
            //return default(T);
        }

        private HttpClient GetClient()
        {
            // For example: https://www.googleapis.com/gmail/v1/users/{userId}/
            var client = new HttpClient { BaseAddress = _baseAddress };

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _tokenManager.GetToken());
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
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
            var resMessage = res.Result;
            string content = res.Result.Content.ReadAsStringAsync().Result;

            if (!resMessage.IsSuccessStatusCode)
            {
                throw new Exception(string.Concat(resMessage.ReasonPhrase, ":", content));
                //var err = JsonConvert.DeserializeObject<ErrorResponse>(content);
            }

            return content;
        }
    }

    /*
{
   "error":{
      "errors":[
         {
            "domain":"global",
            "reason":"invalidParameter",
            "message":"Invalid field selection MessageId",
            "locationType":"parameter",
            "location":"fields"
         }
      ],
      "code":400,
      "message":"Invalid field selection MessageId"
   }
}
*/
}

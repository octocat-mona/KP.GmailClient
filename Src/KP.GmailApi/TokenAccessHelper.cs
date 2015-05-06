using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using KP.GmailApi.Models;
using Newtonsoft.Json;

namespace KP.GmailApi
{
    /// <summary>
    /// A helper class to retrieve an access token for a user.
    /// </summary>
    public class TokenAccessHelper
    {
        private readonly string _authorizationServerUrl;
        private readonly string _clientId;
        private readonly string _clientSecret;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizationServerUrl"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        public TokenAccessHelper(string authorizationServerUrl, string clientId, string clientSecret)
        {
            _authorizationServerUrl = authorizationServerUrl;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        /// <summary>
        /// Get an authorization code using the Gmail modify, readonly and compose scopes.
        /// </summary>
        /// <returns></returns>
        public string GetAuthorizationCode()
        {
            string url = string.Concat("https://accounts.google.com/o/oauth2/auth",
                "?client_id=", HttpUtility.UrlEncode(_clientId),
                "&redirect_uri=", HttpUtility.UrlEncode("http://localhost"),
                "&response_type=", HttpUtility.UrlEncode("code"),
                "&scope=", HttpUtility.UrlEncode("https://www.googleapis.com/auth/gmail.modify https://www.googleapis.com/auth/gmail.readonly https://www.googleapis.com/auth/gmail.compose"),
                "&access_type=", HttpUtility.UrlEncode("offline")
                );

            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            var res = client.GetAsync(url).Result;
            string content = res.Content.ReadAsStringAsync().Result;

            res.EnsureSuccessStatusCode();

            var file = new FileInfo("login.html");

            File.WriteAllText(file.FullName, content);
            Process.Start(file.FullName);

            Console.WriteLine("Enter URl after accepting request:");
            Console.WriteLine(Environment.NewLine);

            var resultUrl = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(resultUrl))
                throw new Exception("User did not enter valid Authorization code");

            var query = new Uri(resultUrl).Query;
            string code = HttpUtility.ParseQueryString(query).Get("code");

            if (string.IsNullOrWhiteSpace(code))
                throw new Exception("No valid code found");

            return code;
        }

        public Oauth2Token GetToken(string authorizationCode)
        {
            string content = string.Concat(
                "code=", HttpUtility.UrlEncode(authorizationCode),
                "&client_id=", HttpUtility.UrlEncode(_clientId),
                "&client_secret=", HttpUtility.UrlEncode(_clientSecret),
                "&redirect_uri=", HttpUtility.UrlEncode("http://localhost"),
                "&grant_type=", HttpUtility.UrlEncode("authorization_code")
                );

            var stringContent = new StringContent(content);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var client = new HttpClient();
            var result = client.PostAsync(_authorizationServerUrl, stringContent).Result;
            string json = result.Content.ReadAsStringAsync().Result;

            result.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Oauth2Token>(json);
        }
    }
}

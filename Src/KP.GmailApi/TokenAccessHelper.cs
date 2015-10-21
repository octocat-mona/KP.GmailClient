using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using KP.GmailApi.Managers;
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
        private bool _waitingForResponse;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        public TokenAccessHelper(string clientId, string clientSecret)
        {
            _authorizationServerUrl = OAuth2TokenManager.AuthorizationServerUrl;
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

            //TODO: WIP
            //ListenForResponse();

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

        private void ListenForResponse()
        {
            var listener = new HttpListener();
            try
            {
                listener.Prefixes.Add("http://*:80/");

                listener.Start();
                while (_waitingForResponse)
                {
                    var context = listener.GetContext();
                    Task.Run(() => ProcessRequest(context));
                }
            }
            finally
            {
                listener.Abort();
            }
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            try
            {
                _waitingForResponse = false;
                //context.Request.Url;//http://localhost/?code=4/CKYxlJc64Ag_UYOB25qEIDEhjV2zYnU6FxKcZRAIiWA.AvsvczEHfp4aWmFiZwPfH02HPEqUmgI

                string text = "test";
                byte[] bytes = Encoding.UTF8.GetBytes(text);

                context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                context.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
                byte[] bytes = Encoding.UTF8.GetBytes(ex.ToString());

                context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                context.Response.StatusCode = 500;
            }
            finally
            {
                context.Response.Close();
            }
        }

        /// <summary>
        /// Exchange the authorization code into a refresh token.
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <returns>A refresh token</returns>
        public string GetRefreshToken(string authorizationCode)
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

            return JsonConvert.DeserializeObject<OAuth2Token>(json).RefreshToken;
        }
    }
}

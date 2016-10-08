using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        /// <summary>View and modify but not delete your email</summary>
        private const string ScopeModify = "https://www.googleapis.com/auth/gmail.modify";
        /// <summary>View your emails messages and settings</summary>
        private const string ScopeReadonly = "https://www.googleapis.com/auth/gmail.readonly";
        /// <summary>Manage drafts and send emails</summary>
        private const string ScopeCompose = "https://www.googleapis.com/auth/gmail.compose";
        private readonly string _authorizationServerUrl;
        private readonly string _clientId;
        private readonly string _clientSecret;

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
                "&scope=", HttpUtility.UrlEncode(string.Join(" ", ScopeModify, ScopeReadonly, ScopeCompose)),
                "&access_type=", HttpUtility.UrlEncode("offline")
                );

            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            var res = client.GetAsync(url).Result;
            string content = res.Content.ReadAsStringAsync().Result;

            res.EnsureSuccessStatusCode();

            var file = new FileInfo("login.html");
            File.WriteAllText(file.FullName, content);
            // Start the browser to let the user login
            Process.Start(file.FullName);

            return RequestAuthorizationCode();
        }

        private static string RequestAuthorizationCode()
        {
            using (var listener = new HttpListener())
            {
                listener.Prefixes.Add("http://*:80/");
                listener.Start();

                while (true)
                {
                    var context = listener.GetContext();
                    try
                    {
                        if (!context.Request.IsLocal)
                            throw new Exception("URL should be localhost");

                        var uri = context.Request.Url;
                        var queryStringCollection = HttpUtility.ParseQueryString(uri.Query);

                        // Example: http://localhost/?error=access_denied#
                        string error = queryStringCollection["error"];
                        if (error != null && string.Equals(error, "access_denied", StringComparison.OrdinalIgnoreCase))
                            throw new Exception("User denied request");

                        string code = queryStringCollection.Get("code");
                        if (string.IsNullOrWhiteSpace(code))
                            throw new Exception("No valid code found");

                        context.Response.StatusCode = 200;
                        return code;
                    }
                    finally
                    {
                        context.Response.Close();
                    }
                }
            }
        }

        /*private string ProcessRequest(HttpListenerContext context)
        {
            try
            {
                if (!context.Request.IsLocal)
                    throw new Exception("URL should be localhost");

                var uri = context.Request.Url;
                var queryStringCollection = HttpUtility.ParseQueryString(uri.Query);

                //http://localhost/?error=access_denied#
                string error = queryStringCollection["error"];
                if (error != null && string.Equals(error, "access_denied", StringComparison.OrdinalIgnoreCase))
                    throw new Exception("User denied request");

                string code = queryStringCollection.Get("code");
                if (string.IsNullOrWhiteSpace(code))
                    throw new Exception("No valid code found");

                //_code = code;
                context.Response.StatusCode = 200;
                return code;
            }
            finally
            {
                context.Response.Close();
            }
        }

        private bool ProcessRequest2(HttpListenerContext context)
        {
            try
            {
                if (!context.Request.IsLocal)
                    throw new Exception("URL should be localhost");

                var uri = context.Request.Url;
                var queryStringCollection = HttpUtility.ParseQueryString(uri.Query);

                //http://localhost/?error=access_denied#
                string error = queryStringCollection["error"];
                if (error != null && string.Equals(error, "access_denied", StringComparison.OrdinalIgnoreCase))
                    throw new Exception("User denied request");

                string code = queryStringCollection.Get("code");
                if (string.IsNullOrWhiteSpace(code))
                    throw new Exception("No valid code found");

                //_code = code;
                context.Response.StatusCode = 200;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
                context.Response.StatusCode = 500;
                //_code = null;

                return false;
            }
            finally
            {
                context.Response.Close();
            }
        }*/

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

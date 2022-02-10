using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using KP.GmailClient.Authentication.Dtos;
using KP.GmailClient.Authentication.TokenClients;
using KP.GmailClient.Builders;
using KP.GmailClient.Models;

namespace KP.GmailClient.Authentication
{
    public class GmailAuthenticationBroker
    {
        public TimeSpan DefaultTimeout { get; set; } = TimeSpan.FromMinutes(5);

        /// <summary>Opens the users default browser to show the consent screen and exchange tokens.</summary>
        /// <param name="clientCredentialsFile">The client credentials file as downloaded from the Google Cloud Console.</param>
        /// <param name="scopes">The requested Gmail scopes</param>
        /// <param name="timeout">The timeout in which the user has to complete the flow, defaults to <see cref="DefaultTimeout"/>.</param>
        /// <returns></returns>
        public async Task<OAuth2Token> AuthenticateAsync(string clientCredentialsFile, GmailScopes scopes, TimeSpan timeout = default)
        {
            OAuth2ClientCredentials credentials;
            using (var stream = File.OpenRead(clientCredentialsFile))
            {
                credentials = (await JsonSerializer.DeserializeAsync<OAuth2ClientCredentialsWrapper>(stream))?.Credentials
                              ?? throw new ArgumentException("Invalid credentials file", nameof(clientCredentialsFile));
            }

            int port = GetAvailablePort();
            string redirectUri = $"http://localhost:{port}/authorize/";
            string state = Guid.NewGuid().ToString("N");

            string queryString = new OAuthQueryStringBuilder(credentials, redirectUri)
                .WithScopes(scopes)
                .WithState(state)
                .Build();

            // Open the default browser to start the authentication flow
            Process.Start(new ProcessStartInfo
            {
                FileName = $"{credentials.AuthUri}{queryString}",
                UseShellExecute = true
            });

            var listenTask = ListenForResponseAsync(redirectUri);
            var authResponse = await GetResponse(listenTask, timeout).ConfigureAwait(false);
            EnsureValidResponse(authResponse, state);

            using var tokenClient = new TokenClient(credentials);
            var token = await tokenClient.ExchangeCodeAsync(redirectUri, authResponse.Code).ConfigureAwait(false);
            return token;
        }

        /// <summary>Wait for the response or timeout.</summary>
        /// <exception cref="TimeoutException">When the listen task timed out.</exception>
        private async Task<AuthorizationResponse> GetResponse(Task<AuthorizationResponse> listenTask, TimeSpan timeout)
        {
            timeout = timeout == default ? DefaultTimeout : timeout;
            var delayTask = Task.Delay(timeout);
            await Task.WhenAny(listenTask, delayTask).ConfigureAwait(false);

            if (listenTask.Status == TaskStatus.RanToCompletion)
            {
                return listenTask.Result;
            }

            throw new TimeoutException($"Authorization flow was not completed within the specified timeout {timeout:g}");
        }

        private static async Task<AuthorizationResponse> ListenForResponseAsync(string uri)
        {
            using var httpListener = new HttpListener { Prefixes = { uri } };
            httpListener.Start();

            // Start listening for a response
            var context = await httpListener.GetContextAsync().ConfigureAwait(false);

            var authorizationResponse = new AuthorizationResponse
            {
                Code = context.Request.QueryString["code"],
                State = context.Request.QueryString["state"],
                Error = context.Request.QueryString["error"],
                ErrorDescription = context.Request.QueryString["error_description"],
                ErrorUri = context.Request.QueryString["error_uri"]
            };

            byte[] bytes = GetHtmlResponseBytes(authorizationResponse);
            var response = context.Response;
            response.ContentLength64 = bytes.Length;
            response.KeepAlive = false;
            await response.OutputStream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
            response.Close();

            return authorizationResponse;
        }

        private static byte[] GetHtmlResponseBytes(AuthorizationResponse response)
        {
            string html = $@"
<html>
    <body>
        <h1>{response}</h1>
        <p>You can now close this window.</p>
    </body>
</html>
";

            return Encoding.UTF8.GetBytes(html);
        }

        private static void EnsureValidResponse(AuthorizationResponse response, string state)
        {
            if (!response.IsSuccess)
            {
                throw new GoogleAuthorizationException($"{response}");
            }

            if (response.State != state)
            {
                throw new GoogleAuthorizationException($"Received invalid state '{response.State}'");
            }
        }

        private static int GetAvailablePort()
        {
            using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
            int port = ((IPEndPoint)socket.LocalEndPoint).Port;
            return port;
        }
    }
}

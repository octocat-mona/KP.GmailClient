using KP.GmailApi.Managers;
using KP.GmailApi.Models;
using KP.GmailApi.ServiceExtensions;
using KP.GmailApi.Services;

namespace KP.GmailApi.WinFormsSample
{
    internal class Readme
    {
        public static void Samples()
        {
            // ----------------------
            // --- SETUP ---
            // ----------------------

            string clientId = "";
            string clientSecret = "";

            var tokenManager = new OAuth2TokenManager(clientId, clientSecret);
            // Provide a refresh token (required once)
            if (!tokenManager.HasTokenSetup())
            {
                // Client ID and secret of your project,
                // see the Dev Console (https://console.developers.google.com/project)
                var tokenHelper = new TokenAccessHelper(clientId, clientSecret);

                // Get a refresh token, launches a browser for user interaction:
                string authCode = tokenHelper.GetAuthorizationCode();
                string refreshToken = tokenHelper.GetRefreshToken(authCode);

                // First time required only
                tokenManager.Setup(refreshToken, false);
            }

            var service = new GmailClient(tokenManager);

            // ----------------------
            // --- USAGE EXAMPLES ---
            // ----------------------
            // Get the users profile
            service.GetProfile();

            // Get inbox messages
            service.Messages.List();

            // Get starred messages
            service.Messages.ListByLabel(Label.Starred);

            // List all labels
            service.Labels.List();
        }
    }
}

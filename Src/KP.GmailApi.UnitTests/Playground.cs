using System.Collections.Generic;
using System.Threading.Tasks;
using KP.GmailApi.Managers;
using KP.GmailApi.Models;
using KP.GmailApi.ServiceExtensions;
using KP.GmailApi.UnitTests.IntegrationTests;
using Xunit;

namespace KP.GmailApi.UnitTests
{
    public class Playground
    {
        //[Fact]
        public static async Task Play()
        {
            // ----------------------
            // --- SETUP ---
            // ----------------------

            string clientId = SettingsManager.GetClientId();
            string clientSecret = SettingsManager.GetClientSecret();

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
            Profile profile = await service.GetProfileAsync();

            // Get inbox messages
            IList<Message> messages = await service.Messages.ListAsync();

            // Get starred messages
            IList<Message> starredMessages = await service.Messages.ListByLabelAsync(Label.Starred);

            // List all labels
            IList<Label> labels = await service.Labels.ListAsync();

            // List all drafts
            IList<Draft> drafts = await service.Drafts.ListAsync();


            // ----------------------
            // --- EXTRA USAGE EXAMPLES ---
            // ----------------------
            string refreshToken2 = SettingsManager.GetRefreshToken();
            tokenManager.Setup(refreshToken2, true);
        }
    }
}

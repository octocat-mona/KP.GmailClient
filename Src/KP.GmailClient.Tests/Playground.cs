using System.Collections.Generic;
using System.Threading.Tasks;
using KP.GmailClient.Models;
using KP.GmailClient.Services.Extensions;
using KP.GmailClient.Tests.IntegrationTests;

namespace KP.GmailClient.Tests
{
    public class Playground
    {
        public async Task PlayAsync()
        {
            // ----------------------
            // --- SETUP ---
            // ----------------------

            // Either use from config
            const GmailScopes scopes = GmailScopes.Readonly | GmailScopes.Compose;
            string privateKey = SettingsManager.GetPrivateKey();
            string tokenUri = SettingsManager.GetTokenUri();
            string clientEmail = SettingsManager.GetClientEmail();
            string emailAddress = SettingsManager.GetEmailAddress();
            var accountCredential = new ServiceAccountCredential
            {
                PrivateKey = privateKey,
                TokenUri = tokenUri,
                ClientEmail = clientEmail
            };

            // Or use from downloaded JSON file directly
            const string path = "C:\\Users\\Me\\Documents\\Gmail-Project.json";
            var initializer = GmailClientInitializer.Initialize(path, scopes);
            var client2 = new GmailClient(initializer, emailAddress);

            using (var client = new GmailClient(accountCredential, emailAddress, scopes))
            {
                // ----------------------
                // --- USAGE EXAMPLES ---
                // ----------------------
                // Send a plain text email
                Message sentMessage = await client.Messages.SendAsync(emailAddress, "The subject", "Plain text body");

                // Send a HTML email
                sentMessage = await client.Messages.SendAsync(emailAddress, "The subject", "<h1>HTML body</h1>", isBodyHtml: true);

                // Get the users profile
                Profile profile = await client.GetProfileAsync();

                // Get inbox messages
                IList<Message> messages = await client.Messages.ListAsync();

                // Get starred messages
                IList<Message> starredMessages = await client.Messages.ListByLabelAsync(Label.Starred);

                // List all labels
                IList<Label> labels = await client.Labels.ListAsync();

                // List all drafts
                IList<Draft> drafts = await client.Drafts.ListAsync();
            }
        }
    }
}
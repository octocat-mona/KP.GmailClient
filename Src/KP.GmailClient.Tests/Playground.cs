using System.Collections.Generic;
using System.Threading.Tasks;
using KP.GmailClient.Models;
using KP.GmailClient.Services.Extensions;
using KP.GmailClient.Tests.IntegrationTests;
using Xunit;

namespace KP.GmailClient.Tests
{
    public class Playground
    {
        public async Task Play()
        {
            // ----------------------
            // --- PREREQUISITES ---
            // ----------------------

#if DOCS
            1. Create a new project in the Google Cloud Platform -> https://console.cloud.google.com/home/dashboard
            2. Enable the Gmail API.
            3. Create a service account for the project -> https://console.cloud.google.com/iam-admin/serviceaccounts/
            4. Create and download a new key as JSON file.
            5. (only for G Suite users) Go to the G Suite Admin console and select the scopes, more on that here https://developers.google.com/identity/protocols/OAuth2ServiceAccount#delegatingauthority
#endif

            // ----------------------
            // --- SETUP ---
            // ----------------------

            // Either use from config
            const GmailScopes scopes = GmailScopes.Readonly | GmailScopes.Compose;
            string privateKey = SettingsManager.GetPrivateKey();
            string tokenUri = SettingsManager.GetTokenUri();
            string clientEmail = SettingsManager.GetClientEmail();
            string emailAddress = SettingsManager.GetEmailAddress();
            ServiceAccountCredential accountCredential = new ServiceAccountCredential
            {
                PrivateKey = privateKey,
                TokenUri = tokenUri,
                ClientEmail = clientEmail
            };
            var client = new GmailClient(accountCredential, emailAddress, scopes);

            // Or use from downloaded JSON file directly
            const string path = "C:\\Users\\Me\\Documents\\Gmail-Project.json";
            var initializer = GmailClientInitializer.Initialize(path, scopes);
            client = new GmailClient(initializer, emailAddress);

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
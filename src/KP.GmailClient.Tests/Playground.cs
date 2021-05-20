using System.Collections.Generic;
using System.Threading.Tasks;
using KP.GmailClient.Authentication;
using KP.GmailClient.Authentication.TokenClients;
using KP.GmailClient.Authentication.TokenStores;
using KP.GmailClient.Models;
using KP.GmailClient.Services;
using Xunit;

namespace KP.GmailClient.Tests
{
    public class Playground
    {
        private const string ClientCredentialsFile = "oauth_client_credentials.json";
        private const string TokenFile = "token.json";

        [Fact(Skip = "Manual test")]
        public async Task AuthenticateAsync()
        {
            var broker = new GmailAuthenticationBroker();
            const GmailScopes scopes = GmailScopes.FullAccess;
            var token = await broker.AuthenticateAsync(ClientCredentialsFile, scopes);

            var tokenStore = new FileTokenStore(TokenFile);
            await tokenStore.StoreTokenAsync(token);
        }

        [Fact(Skip = "Manual test")]
        public async Task PlayAsync()
        {
            var tokenClient = TokenClient.Create(ClientCredentialsFile);
            var tokenStore = new FileTokenStore(TokenFile);
            using var client = new GmailClient(tokenClient, tokenStore);

            // ----------------------
            // --- USAGE EXAMPLES ---
            // ----------------------

            // Send a plain text email
            Message plainMessage = await client.Messages.SendAsync("example@gmail.com", "Subject", "Plain text body");

            // Send a HTML email
            Message htmlMessage = await client.Messages.SendAsync("example@gmail.com", "Subject", "<h1>HTML body</h1>", isBodyHtml: true);

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
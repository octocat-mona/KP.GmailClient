using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using KP.GmailApi.Models;
using KP.GmailApi.Services.Extensions;
using KP.GmailApi.UnitTests.IntegrationTests;
using Xunit;

namespace KP.GmailApi.UnitTests
{
    [SuppressMessage("", "CS0219", Justification = "Just samples")]
    public class Playground
    {
        public static string KeyFile => SettingsManager.GetGoogleAccountCredentialsFile();
        public static string EmailAddress => SettingsManager.GetEmailAddress();

        [Fact(Skip = "Playground")]
        public async Task Play()
        {
            // ----------------------
            // --- PREREQUISITES ---
            // ----------------------

#if DOCS
            1. Create a new project in the Google Developer Console -> https://console.developers.google.com/project
            2. Create a service account for the project -> https://console.cloud.google.com/iam-admin/serviceaccounts/
            3. Create and download a new key as JSON file.
#endif

            // ----------------------
            // --- SETUP ---
            // ----------------------

            var service = new GmailClient(KeyFile, EmailAddress, GmailScopes.Readonly);

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

        }
    }
}

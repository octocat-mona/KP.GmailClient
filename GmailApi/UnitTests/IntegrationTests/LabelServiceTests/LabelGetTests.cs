using System;
using GmailApi;
using GmailApi.Services;
using NUnit.Framework;

namespace UnitTests.IntegrationTests.LabelServiceTests
{
    class LabelGetTests
    {
        private readonly LabelService _labels;

        public LabelGetTests()
        {
            var tokenManager = new TokenManager(SettingsManager.GetClientId(), SettingsManager.GetClientSecret());
            var gmailClient = new GmailClient(SettingsManager.GetEmailAddress(), tokenManager);

            _labels = new LabelService(gmailClient);
        }

    }
}

using Xunit;

namespace KP.GmailClient.Tests.IntegrationTests
{
    public class SmokeTests
    {
        [Fact]
        public void HasSettingsConfigured()
        {
            // Assert
            var proxy = SettingsManager.GmailProxy;
        }
    }
}

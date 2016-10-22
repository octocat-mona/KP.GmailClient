using Xunit;

namespace KP.GmailApi.Tests.IntegrationTests
{
    public class SmokeTests
    {
        [Fact]
        public void HasSettingsConfigured()
        {
            // Assert
            SettingsManager.GetGmailProxy();
        }
    }
}

using Xunit;

namespace KP.GmailApi.UnitTests.IntegrationTests
{
    public class SmokeTests
    {
        [Fact]
        public void HasSettingsConfigured()
        {
            // Assert
            SettingsManager.GetClientId();
            SettingsManager.GetClientSecret();
            //SettingsManager.GetEmailAddress();
            SettingsManager.GetRefreshToken();
        }
    }
}

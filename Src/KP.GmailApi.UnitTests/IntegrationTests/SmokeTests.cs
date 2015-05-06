using Xunit;

namespace UnitTests.IntegrationTests
{
    public class SmokeTests
    {
        [Fact]
        public void HasSettingsConfigured()
        {
            // Assert
            SettingsManager.GetClientId();
            SettingsManager.GetClientSecret();
            SettingsManager.GetEmailAddress();
            SettingsManager.GetRefreshToken();
        }
    }
}

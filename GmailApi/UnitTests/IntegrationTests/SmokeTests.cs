using Shouldly;
using Xunit;

namespace UnitTests.IntegrationTests
{
    public class SmokeTests
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _emailAddress;
        private readonly string _refreshToken;

        public SmokeTests()
        {
            _clientId = SettingsManager.GetClientId();
            _clientSecret = SettingsManager.GetClientSecret();
            _emailAddress = SettingsManager.GetEmailAddress();
            _refreshToken = SettingsManager.GetRefreshToken();
        }

        [Fact]
        public void HasSettingsConfigured()
        {
            // Assert
            _clientId.ShouldNotBeNullOrEmpty();
            _clientSecret.ShouldNotBeNullOrEmpty();
            _emailAddress.ShouldNotBeNullOrEmpty();
            _refreshToken.ShouldNotBeNullOrEmpty();
        }
    }
}

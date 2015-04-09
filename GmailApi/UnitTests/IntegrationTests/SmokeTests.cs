using GmailApi;
using NUnit.Framework;

namespace UnitTests.IntegrationTests
{
    public class SmokeTests
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _emailAddress;

        public SmokeTests()
        {
            _clientId = SettingsManager.GetClientId();
            _clientSecret = SettingsManager.GetClientSecret();
            _emailAddress = SettingsManager.GetEmailAddress();
        }

        //[Test]
        public void HasSettingsConfigured()
        {
            // Assert
            Assert.IsNotNullOrEmpty(_clientId, "CliendId not configured in config");
            Assert.IsNotNullOrEmpty(_clientSecret, "CliendSecret not configured in config");
            Assert.IsNotNullOrEmpty(_emailAddress, "Email not configured in config");
        }

        //[Test]
        public void HasTokenConfigured()
        {
            // Arrange
            var tokenManager = new TokenManager(_clientId, _clientSecret);

            // Act
            bool hasTokenConfigured = tokenManager.HasTokenConfigured();

            // Assert
            Assert.IsTrue(hasTokenConfigured);
        }
    }
}

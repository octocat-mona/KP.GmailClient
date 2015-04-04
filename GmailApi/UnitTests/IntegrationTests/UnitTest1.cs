using System.Configuration;
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
            _clientId = ConfigurationManager.AppSettings["ClientId"];
            _clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            _emailAddress = ConfigurationManager.AppSettings["EmailAddress"];
        }

        [Test]
        public void HasTokenSetup()
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

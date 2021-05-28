using FluentAssertions;
using Xunit;

namespace KP.GmailClient.IntegrationTests
{
    public class SmokeTests
    {
        [Fact]
        public void HasSettingsConfigured()
        {
            // Act
            var proxy = SettingsManager.GmailProxy;

            // Assert
            proxy.Should().NotBeNull();
        }
    }
}

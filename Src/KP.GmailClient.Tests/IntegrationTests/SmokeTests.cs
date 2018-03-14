using FluentAssertions;
using Xunit;

namespace KP.GmailClient.Tests.IntegrationTests
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

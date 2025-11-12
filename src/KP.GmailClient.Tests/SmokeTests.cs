using AwesomeAssertions;
using Xunit;

namespace KP.GmailClient.IntegrationTests;

public class SmokeTests : IClassFixture<GlobalDelayFixture>
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